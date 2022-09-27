using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Models;

namespace ReferenceManager.App.Controllers
{
    public class AccesoesController : Controller
    {
        private readonly DBReferenciasContext _context;

        public AccesoesController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: Accesoes
        public async Task<IActionResult> Index()
        {
            return _context.Accesos != null ?
                        View(await _context.Accesos.ToListAsync()) :
                        Problem("Entity set 'DBReferenciasContext.Accesos'  is null.");
        }

        // GET: Accesoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accesos == null)
            {
                return NotFound();
            }

            var acceso = await _context.Accesos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acceso == null)
            {
                return NotFound();
            }

            return View(acceso);
        }

        // GET: Accesoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accesoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Activio,Modulo,Url")] Acceso acceso)
        {

            _context.Add(acceso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(acceso);
        }

        // GET: Accesoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accesos == null)
            {
                return NotFound();
            }

            var acceso = await _context.Accesos.FindAsync(id);
            if (acceso == null)
            {
                return NotFound();
            }
            return View(acceso);
        }

        // POST: Accesoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Activio,Modulo,Url")] Acceso acceso)
        {
            if (id != acceso.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(acceso);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccesoExists(acceso.Id))
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

        // GET: Accesoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accesos == null)
            {
                return NotFound();
            }

            var acceso = await _context.Accesos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acceso == null)
            {
                return NotFound();
            }

            return View(acceso);
        }

        // POST: Accesoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accesos == null)
            {
                return Problem("Entity set 'DBReferenciasContext.Accesos'  is null.");
            }
            var acceso = await _context.Accesos.FindAsync(id);
            if (acceso != null)
            {
                _context.Accesos.Remove(acceso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccesoExists(int id)
        {
            return (_context.Accesos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
