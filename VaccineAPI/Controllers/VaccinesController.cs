using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineAPI.Models;

namespace VaccineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinesController : ControllerBase
    {
        private readonly VaccineContext _context;
        private readonly UserManager<User> _userManager;

        public VaccinesController(VaccineContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Vaccines
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<IEnumerable<Vaccine>>> GetVaccineItems()
        {
            return await _context.Vaccines.ToListAsync();
        }

        // POST: api/Vaccines/order
        [HttpPost("order")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Order>> Order(OrderViewModel order)
        {
            var vaccineOrder = new VaccineOrder
            {
                Vaccine = await _context.Vaccines.FindAsync(order.VaccineId),
                Order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    User = await _userManager.FindByIdAsync(order.UserId),
                    Quantity = order.Quantity,
                    Date = DateTime.Now,
                    Status = "Ordered"
                }
            };

            await _context.VaccineOrders.AddAsync(vaccineOrder);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/Vaccines/history
        [HttpGet("history")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<IEnumerable<VaccineOrder>>> GetOrderItems()
        {
            var result = await _context.VaccineOrders
                .Include(x => x.Order).ThenInclude(x => x.User)
                .Include(x => x.Vaccine)
                .OrderByDescending(x => x.Order.Date)
                .ToListAsync();

            return result;
        }

        // GET: api/Vaccines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaccine>> GetVaccine(Guid id)
        {
            var vaccine = await _context.Vaccines.FindAsync(id);

            if (vaccine == null)
            {
                return NotFound();
            }

            return vaccine;
        }

        // PUT: api/Vaccines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaccine(Guid id, Vaccine vaccine)
        {
            if (id != vaccine.VaccineId)
            {
                return BadRequest();
            }

            _context.Entry(vaccine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vaccines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vaccine>> PostVaccine(Vaccine vaccine)
        {
            _context.Vaccines.Add(vaccine);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetVaccine", new { id = vaccine.VaccineId }, vaccine);
            return CreatedAtAction(nameof(GetVaccine), new { id = vaccine.VaccineId }, vaccine);
        }

        // DELETE: api/Vaccines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vaccine>> DeleteVaccine(Guid id)
        {
            var vaccine = await _context.Vaccines.FindAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            _context.Vaccines.Remove(vaccine);
            await _context.SaveChangesAsync();

            return vaccine;
        }

        private bool VaccineExists(Guid id)
        {
            return _context.Vaccines.Any(e => e.VaccineId == id);
        }
    }
}
