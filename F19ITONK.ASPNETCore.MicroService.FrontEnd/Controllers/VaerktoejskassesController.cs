using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using F19ITONK.ASPNETCore.MicroService.ClassLib.Models;
using F19ITONK.ASPNETCore.MicroService.FrontEnd.Models;

namespace F19ITONK.ASPNETCore.MicroService.FrontEnd.Controllers
{
    public class VaerktoejskassesController : Controller
    {
        private readonly FrontEndContext _context;

        public VaerktoejskassesController(FrontEndContext context)
        {
            _context = context;
        }

        // GET: Vaerktoejskasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vaerktoejskasse.ToListAsync());
        }

        // GET: Vaerktoejskasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoejskasse = await _context.Vaerktoejskasse
                .FirstOrDefaultAsync(m => m.VTKId == id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            return View(vaerktoejskasse);
        }

        // GET: Vaerktoejskasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaerktoejskasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VTKId,VTKAnskaffet,VTKFabrikat,VTKEjesAf,VTKModel,VTKSerienummer,VTKFarve")] Vaerktoejskasse vaerktoejskasse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaerktoejskasse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaerktoejskasse);
        }

        // GET: Vaerktoejskasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoejskasse = await _context.Vaerktoejskasse.FindAsync(id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }
            return View(vaerktoejskasse);
        }

        // POST: Vaerktoejskasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VTKId,VTKAnskaffet,VTKFabrikat,VTKEjesAf,VTKModel,VTKSerienummer,VTKFarve")] Vaerktoejskasse vaerktoejskasse)
        {
            if (id != vaerktoejskasse.VTKId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaerktoejskasse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaerktoejskasseExists(vaerktoejskasse.VTKId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vaerktoejskasse);
        }

        // GET: Vaerktoejskasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoejskasse = await _context.Vaerktoejskasse
                .FirstOrDefaultAsync(m => m.VTKId == id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            return View(vaerktoejskasse);
        }

        // POST: Vaerktoejskasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaerktoejskasse = await _context.Vaerktoejskasse.FindAsync(id);
            _context.Vaerktoejskasse.Remove(vaerktoejskasse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaerktoejskasseExists(int id)
        {
            return _context.Vaerktoejskasse.Any(e => e.VTKId == id);
        }
    }
}
