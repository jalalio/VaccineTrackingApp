using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VaccineAPI.Models;
using VaccineSER.DTOs;
using VaccineSER.Interfaces;

namespace VaccineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IOptions<EmailOptionsDTO> _emailOptions;
        private readonly IEmail _email;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config,
            IOptions<EmailOptionsDTO> emailOptions, IEmail email)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginViewModel model)
        {
            //var user = await _userManager.FindByNameAsync(model.Email);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return BadRequest();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if(!result.Succeeded)
            {
                return BadRequest(result);
            }
            return Ok(new
            {
                result = result,
                //token = JwtTokenGeneratorMachine(user)
                token = JwtTokenGeneratorMachine(user).Result // Just returns the result i.e. token
            });
        }

        // POST: api/auth/resetpassword
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null || user.EmailConfirmed)
            {
                //Send Email
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var changePasswordUrl = Request.Headers["changePasswordUrl"]; // http://localhost:4200/change-password

                var uriBuilder = new UriBuilder(changePasswordUrl);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["token"] = token;
                query["userid"] = user.Id;
                uriBuilder.Query = query.ToString();
                var urlString = uriBuilder.ToString();

                var emailSubject = "Reset Password";
                var emailBody = $"Click on the link below to change password: <br>{urlString}<br>";
                await _email.Send(model.Email, emailSubject, emailBody, _emailOptions.Value);

                return Ok();
            }

            return Unauthorized();
        }

        // POST: api/auth/changepassword
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, Uri.UnescapeDataString(model.Token), model.NewPassword);

            if (resetPasswordResult.Succeeded)
            {
                return Ok();
            }

            return Unauthorized();
        }

        // POST: api/auth/confirmemail
        [HttpPost("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel model)
        {
            var admin = await _userManager.FindByIdAsync(model.UserId);
            var confirm = await _userManager.ConfirmEmailAsync(admin, Uri.UnescapeDataString(model.Token));

            if (confirm.Succeeded)
            {
                return Ok();
            }

            return Unauthorized();
        }


        private async Task<string> JwtTokenGeneratorMachine(User userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id),
                new Claim(ClaimTypes.Name, userInfo.UserName)
            };

            var roles = await _userManager.GetRolesAsync(userInfo);


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8
             .GetBytes(_config.GetSection("AppSettings:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
