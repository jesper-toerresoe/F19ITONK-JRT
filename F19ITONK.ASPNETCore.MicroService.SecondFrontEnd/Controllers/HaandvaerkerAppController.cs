using F19ITONK.ASPNETCore.MicroService.ClassLib.Models;
using F19ITONK.ASPNETCore.MicroService.SecondFrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace F19ITONK.ASPNETCore.MicroService.SecondFrontEnd.Controllers
{
    public class HaandvaerkerAppController : Controller
    {
        private readonly SecondFrontEndContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient client;

        public HaandvaerkerAppController(SecondFrontEndContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
            client = _clientFactory.CreateClient("backend");

        }

        // GET: HaandvaerkerApp
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Haandvaerker.ToListAsync()); replaced by:
            var response = await client.GetAsync(
            "api/haandvaerker");

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadAsAsync<IEnumerable<Haandvaerker>>();
            return View(result.ToList());
        }

        // GET: HaandvaerkerApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await client.GetAsync(
            "api/haandvaerker/" + id);

            response.EnsureSuccessStatusCode();

            var haandvaerker = await response.Content
                .ReadAsAsync<Haandvaerker>();
            //var haandvaerker = await _context.Haandvaerker
            //    .FirstOrDefaultAsync(m => m.HaandvaerkerId == id);

            if (haandvaerker == null)
            {
                return NotFound();
            }

            return View(haandvaerker);
        }

        // GET: HaandvaerkerApp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HaandvaerkerApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HaandvaerkerId,HVAnsaettelsedato,HVEfternavn,HVFagomraade,HVFornavn")] Haandvaerker haandvaerker)
        {
            if (ModelState.IsValid)
            {

                var response = await client.PostAsJsonAsync("api/haandvaerker", haandvaerker);
                response.EnsureSuccessStatusCode();
                //_context.Add(haandvaerker);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(haandvaerker);
        }

        // GET: HaandvaerkerApp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await client.GetAsync(
            "api/haandvaerker/" + id);

            response.EnsureSuccessStatusCode();

            var haandvaerker = await response.Content
                .ReadAsAsync<Haandvaerker>();


            if (haandvaerker == null)
            {
                return NotFound();
            }

            return View(haandvaerker);
        }

        // POST: HaandvaerkerApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HaandvaerkerId,HVAnsaettelsedato,HVEfternavn,HVFagomraade,HVFornavn")] Haandvaerker haandvaerker)
        {
            if (id != haandvaerker.HaandvaerkerId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                var response = await client.PutAsJsonAsync("api/haandvaerker/" + id, haandvaerker);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(haandvaerker);

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(haandvaerker);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!HaandvaerkerExists(haandvaerker.HaandvaerkerId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(haandvaerker);
        }

        // GET: HaandvaerkerApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await client.GetAsync("api/haandvaerker/" + id);

            response.EnsureSuccessStatusCode();

            var haandvaerker = await response.Content
                .ReadAsAsync<Haandvaerker>();
            //var haandvaerker = await _context.Haandvaerker
            //    .FirstOrDefaultAsync(m => m.HaandvaerkerId == id);

            return View(haandvaerker);
        }

        // POST: HaandvaerkerApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var haandvaerker = await _context.Haandvaerker.FindAsync(id);
            //_context.Haandvaerker.Remove(haandvaerker);
            //await _context.SaveChangesAsync();

            var response = await client.DeleteAsync("api/haandvaerker/" + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));

        }

        //private bool HaandvaerkerExists(int id)
        //{
        //    return _context.Haandvaerker.Any(e => e.HaandvaerkerId == id);
        //}
    }
}
