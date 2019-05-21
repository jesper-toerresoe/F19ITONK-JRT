using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F19ITONK.ASPNETCore.MicroService.Backend.Models;
using F19ITONK.ASPNETCore.MicroService.ClassLib.Models;

namespace F19ITONK.ASPNETCore.MicroService.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaerktoejskasseController : ControllerBase
    {
        private readonly BackendDBContext _context;

        public VaerktoejskasseController(BackendDBContext context)
        {
            _context = context;
        }

        // GET: api/Vaerktoejskasse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaerktoejskasse>>> GetVaerktoejskasse()
        {
            return await _context.Vaerktoejskasse.ToListAsync();
        }

        // GET: api/Vaerktoejskasse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaerktoejskasse>> GetVaerktoejskasse(int id)
        {
            var vaerktoejskasse = await _context.Vaerktoejskasse.FindAsync(id);

            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            return vaerktoejskasse;
        }

        // PUT: api/Vaerktoejskasse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaerktoejskasse(int id, Vaerktoejskasse vaerktoejskasse)
        {
            if (id != vaerktoejskasse.VTKId)
            {
                return BadRequest();
            }

            _context.Entry(vaerktoejskasse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaerktoejskasseExists(id))
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

        // POST: api/Vaerktoejskasse
        [HttpPost]
        public async Task<ActionResult<Vaerktoejskasse>> PostVaerktoejskasse(Vaerktoejskasse vaerktoejskasse)
        {
            _context.Vaerktoejskasse.Add(vaerktoejskasse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaerktoejskasse", new { id = vaerktoejskasse.VTKId }, vaerktoejskasse);
        }

        // DELETE: api/Vaerktoejskasse/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vaerktoejskasse>> DeleteVaerktoejskasse(int id)
        {
            var vaerktoejskasse = await _context.Vaerktoejskasse.FindAsync(id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            _context.Vaerktoejskasse.Remove(vaerktoejskasse);
            await _context.SaveChangesAsync();

            return vaerktoejskasse;
        }

        private bool VaerktoejskasseExists(int id)
        {
            return _context.Vaerktoejskasse.Any(e => e.VTKId == id);
        }
    }
}
