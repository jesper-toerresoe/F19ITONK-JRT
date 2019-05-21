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
    public class VaerktoejController : ControllerBase
    {
        private readonly BackendDBContext _context;

        public VaerktoejController(BackendDBContext context)
        {
            _context = context;
        }

        // GET: api/Vaerktoej
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaerktoej>>> GetVaerktoej()
        {
            return await _context.Vaerktoej.ToListAsync();
        }

        // GET: api/Vaerktoej/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaerktoej>> GetVaerktoej(long id)
        {
            var vaerktoej = await _context.Vaerktoej.FindAsync(id);

            if (vaerktoej == null)
            {
                return NotFound();
            }

            return vaerktoej;
        }

        // PUT: api/Vaerktoej/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaerktoej(long id, Vaerktoej vaerktoej)
        {
            if (id != vaerktoej.VTId)
            {
                return BadRequest();
            }

            _context.Entry(vaerktoej).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaerktoejExists(id))
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

        // POST: api/Vaerktoej
        [HttpPost]
        public async Task<ActionResult<Vaerktoej>> PostVaerktoej(Vaerktoej vaerktoej)
        {
            _context.Vaerktoej.Add(vaerktoej);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaerktoej", new { id = vaerktoej.VTId }, vaerktoej);
        }

        // DELETE: api/Vaerktoej/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vaerktoej>> DeleteVaerktoej(long id)
        {
            var vaerktoej = await _context.Vaerktoej.FindAsync(id);
            if (vaerktoej == null)
            {
                return NotFound();
            }

            _context.Vaerktoej.Remove(vaerktoej);
            await _context.SaveChangesAsync();

            return vaerktoej;
        }

        private bool VaerktoejExists(long id)
        {
            return _context.Vaerktoej.Any(e => e.VTId == id);
        }
    }
}
