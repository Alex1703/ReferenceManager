using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Models;

namespace ReferenceManager.App.Controllers
{
    public class PlantillasController : Controller
    {
        private readonly DBReferenciasContext _context;

        public PlantillasController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: Plantillas
        public async Task<IActionResult> Index()
        {
              return _context.Plantillas != null ? 
                          View(await _context.Plantillas.ToListAsync()) :
                          Problem("Entity set 'DBReferenciasContext.Plantillas'  is null.");
        }

        // GET: Plantillas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Plantillas == null)
            {
                return NotFound();
            }

            var plantilla = await _context.Plantillas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantilla == null)
            {
                return NotFound();
            }

            return View(plantilla);
        }

        // GET: Plantillas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plantillas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Activio,Nombre,Descripcion")] Plantilla plantilla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plantilla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plantilla);
        }

        // GET: Plantillas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Plantillas == null)
            {
                return NotFound();
            }

            var plantilla = await _context.Plantillas.FindAsync(id);
            if (plantilla == null)
            {
                return NotFound();
            }
            return View(plantilla);
        }

        // POST: Plantillas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Activio,Nombre,Descripcion")] Plantilla plantilla)
        {
            if (id != plantilla.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantilla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantillaExists(plantilla.Id))
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
            return View(plantilla);
        }

        // GET: Plantillas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Plantillas == null)
            {
                return NotFound();
            }

            var plantilla = await _context.Plantillas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantilla == null)
            {
                return NotFound();
            }

            return View(plantilla);
        }

        // POST: Plantillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Plantillas == null)
            {
                return Problem("Entity set 'DBReferenciasContext.Plantillas'  is null.");
            }
            var plantilla = await _context.Plantillas.FindAsync(id);
            if (plantilla != null)
            {
                _context.Plantillas.Remove(plantilla);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantillaExists(int id)
        {
          return (_context.Plantillas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
