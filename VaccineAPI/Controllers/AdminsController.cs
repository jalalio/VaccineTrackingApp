using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VaccineAPI.Models;
using VaccineSER.DTOs;
using VaccineSER.Interfaces;

namespace VaccineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly VaccineContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<EmailOptionsDTO> _emailOptions;
        private readonly IEmail _email;

        public bool DisplayConfirmAccountLink { get; set; }

        public AdminsController(VaccineContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            IOptions<EmailOptionsDTO> emailOptions, IEmail email)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailOptions = emailOptions;
            _email = email;
        }

        // POST: api/admins/create
        [HttpPost("create")]
        public async Task<ActionResult<User>> Create(CreateUserViewModel model)
        {
            if (!(await _roleManager.RoleExistsAsync("Admin")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            var admin = new User
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(admin, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            // Add Admin role
            var user = await _userManager.FindByEmailAsync(admin.Email);
            await _userManager.AddToRoleAsync(user, "Admin");

            // Once you add a real email sender, you should remove this code that lets you confirm the account
            DisplayConfirmAccountLink = false;
            // Send confirmation email
            if (DisplayConfirmAccountLink)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(admin);
                var confirmEmailUrl = Request.Headers["confirmEmailUrl"]; // http://localhost:4200/email-confirm

                var uriBuilder = new UriBuilder(confirmEmailUrl);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["token"] = token;
                query["userid"] = admin.Id;
                uriBuilder.Query = query.ToString();
                var urlString = uriBuilder.ToString();

                var emailSubject = "Activate your account";
                var emailBody = $"Please confirm your email by clicking on the link below <br><a href=\"{urlString}\" >Activate</a><br>";
                await _email.Send(model.Email, emailSubject, emailBody, _emailOptions.Value);
            }

            return Ok(result);
        }

    }
}
