using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Models;

namespace ReferenceManager.App.Controllers
{
    public class RefArrendadorViviendumsController : Controller
    {
        private readonly DBReferenciasContext _context;

        public RefArrendadorViviendumsController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: RefArrendadorViviendums
        public async Task<IActionResult> Index()
        {
            return View(await _context.RefArrendadorVivienda.ToListAsync());
        }

        // GET: RefArrendadorViviendums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RefArrendadorVivienda == null)
            {
                return NotFound();
            }

            var refArrendadorViviendum = await _context.RefArrendadorVivienda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (refArrendadorViviendum == null)
            {
                return NotFound();
            }

            return View(refArrendadorViviendum);
        }

        // GET: RefArrendadorViviendums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RefArrendadorViviendums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QueArrienda,QuienVive,EstadoCivil,NombreConyuge,Actividad,TiempoArriendo,DireccionVivienda,CanonArriendo,IncluyeServicios,PuntualResponsable,Concepto")] RefArrendadorViviendum refArrendadorViviendum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refArrendadorViviendum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(refArrendadorViviendum);
        }

        // GET: RefArrendadorViviendums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RefArrendadorVivienda == null)
            {
                return NotFound();
            }

            var refArrendadorViviendum = await _context.RefArrendadorVivienda.FindAsync(id);
            if (refArrendadorViviendum == null)
            {
                return NotFound();
            }
            return View(refArrendadorViviendum);
        }

        // POST: RefArrendadorViviendums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QueArrienda,QuienVive,EstadoCivil,NombreConyuge,Actividad,TiempoArriendo,DireccionVivienda,CanonArriendo,IncluyeServicios,PuntualResponsable,Concepto")] RefArrendadorViviendum refArrendadorViviendum)
        {
            if (id != refArrendadorViviendum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refArrendadorViviendum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefArrendadorViviendumExists(refArrendadorViviendum.Id))
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
            return View(refArrendadorViviendum);
        }

        // GET: RefArrendadorViviendums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RefArrendadorVivienda == null)
            {
                return NotFound();
            }

            var refArrendadorViviendum = await _context.RefArrendadorVivienda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (refArrendadorViviendum == null)
            {
                return NotFound();
            }

            return View(refArrendadorViviendum);
        }

        // POST: RefArrendadorViviendums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RefArrendadorVivienda == null)
            {
                return Problem("Entity set 'DBReferenciasContext.RefArrendadorVivienda'  is null.");
            }
            var refArrendadorViviendum = await _context.RefArrendadorVivienda.FindAsync(id);
            if (refArrendadorViviendum != null)
            {
                _context.RefArrendadorVivienda.Remove(refArrendadorViviendum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RefArrendadorViviendumExists(int id)
        {
            return _context.RefArrendadorVivienda.Any(e => e.Id == id);
        }
    }
}
