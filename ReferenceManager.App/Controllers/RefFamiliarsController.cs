using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Models;

namespace ReferenceManager.App.Controllers
{
    public class RefFamiliarsController : Controller
    {
        private readonly DBReferenciasContext _context;

        public RefFamiliarsController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: RefFamiliars
        public async Task<IActionResult> Index()
        {
            return View(await _context.RefFamiliars.ToListAsync());
        }

        // GET: RefFamiliars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RefFamiliars == null)
            {
                return NotFound();
            }

            var refFamiliar = await _context.RefFamiliars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (refFamiliar == null)
            {
                return NotFound();
            }

            return View(refFamiliar);
        }

        // GET: RefFamiliars/Create
        public IActionResult Create(string Id)
        {
            
            return View();
        }

        // POST: RefFamiliars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConfirmacionNombre,Parentezco,EstadoCivil,NombreConyuge,CantidadHijos,QuienVive,Actividad,TiempoNegocio,BarrioNegocio,CantidadEmpleados,DireccionTelefono,Concepto")] RefFamiliar refFamiliar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refFamiliar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(refFamiliar);
        }

        // GET: RefFamiliars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RefFamiliars == null)
            {
                return NotFound();
            }

            var refFamiliar = await _context.RefFamiliars.FindAsync(id);
            if (refFamiliar == null)
            {
                return NotFound();
            }
            return View(refFamiliar);
        }

        // POST: RefFamiliars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConfirmacionNombre,Parentezco,EstadoCivil,NombreConyuge,CantidadHijos,QuienVive,Actividad,TiempoNegocio,BarrioNegocio,CantidadEmpleados,DireccionTelefono,Concepto")] RefFamiliar refFamiliar)
        {
            if (id != refFamiliar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refFamiliar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefFamiliarExists(refFamiliar.Id))
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
            return View(refFamiliar);
        }

        // GET: RefFamiliars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RefFamiliars == null)
            {
                return NotFound();
            }

            var refFamiliar = await _context.RefFamiliars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (refFamiliar == null)
            {
                return NotFound();
            }

            return View(refFamiliar);
        }

        // POST: RefFamiliars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RefFamiliars == null)
            {
                return Problem("Entity set 'DBReferenciasContext.RefFamiliars'  is null.");
            }
            var refFamiliar = await _context.RefFamiliars.FindAsync(id);
            if (refFamiliar != null)
            {
                _context.RefFamiliars.Remove(refFamiliar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RefFamiliarExists(int id)
        {
            return _context.RefFamiliars.Any(e => e.Id == id);
        }
    }
}
