using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonationBlood.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace DonationBlood.Controllers
{   
    /*[Authorize(Roles =UserRoles.Admin)]*/
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BloodPatientsController : ControllerBase
    {
        private readonly DonationDBContext _context;

        public BloodPatientsController(DonationDBContext context)
        {
            _context = context;
        }

        // GET: api/BloodPatients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodPatient>>> GetBloodPatients()
        {
            return await _context.BloodPatients.ToListAsync();
        }

        // GET: api/BloodPatients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodPatient>> GetBloodPatient(int id)
        {
            var bloodPatient = await _context.BloodPatients.FindAsync(id);

            if (bloodPatient == null)
            {
                return NotFound();
            }

            return bloodPatient;
        }

        // PUT: api/BloodPatients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodPatient(int id, BloodPatient bloodPatient)
        {
            if (id != bloodPatient.id)
            {
                return BadRequest();
            }

            _context.Entry(bloodPatient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodPatientExists(id))
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

        // POST: api/BloodPatients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BloodPatient>> PostBloodPatient(BloodPatient bloodPatient)
        {
            _context.BloodPatients.Add(bloodPatient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloodPatient", new { id = bloodPatient.id }, bloodPatient);
        }

        // DELETE: api/BloodPatients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BloodPatient>> DeleteBloodPatient(int id)
        {
            var bloodPatient = await _context.BloodPatients.FindAsync(id);
            if (bloodPatient == null)
            {
                return NotFound();
            }

            _context.BloodPatients.Remove(bloodPatient);
            await _context.SaveChangesAsync();

            return bloodPatient;
        }

        private bool BloodPatientExists(int id)
        {
            return _context.BloodPatients.Any(e => e.id == id);
        }
    }
}
