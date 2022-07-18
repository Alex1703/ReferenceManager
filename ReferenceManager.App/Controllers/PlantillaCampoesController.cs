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
    public class PlantillaCampoesController : Controller
    {
        private readonly DBReferenciasContext _context;

        public PlantillaCampoesController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: PlantillaCampoes
        public async Task<IActionResult> Index()
        {
            var dBReferenciasContext = _context.PlantillaCampos.Include(p => p.FkCampoNavigation).Include(p => p.FkPlantillaNavigation);
            return View(await dBReferenciasContext.ToListAsync());
        }

        // GET: PlantillaCampoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlantillaCampos == null)
            {
                return NotFound();
            }

            var plantillaCampo = await _context.PlantillaCampos
                .Include(p => p.FkCampoNavigation)
                .Include(p => p.FkPlantillaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantillaCampo == null)
            {
                return NotFound();
            }

            return View(plantillaCampo);
        }

        // GET: PlantillaCampoes/Create
        public IActionResult Create()
        {
            ViewData["FkCampo"] = new SelectList(_context.Campos, "Id", "Nombre");
            ViewData["FkPlantilla"] = new SelectList(_context.Plantillas, "Id", "Nombre");
            return View();
        }

        // POST: PlantillaCampoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FkCampo,FkPlantilla,OrdenCampo,EtiquetaCampo,Logituud,Requerido")] PlantillaCampo plantillaCampo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plantillaCampo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCampo"] = new SelectList(_context.Campos, "Id", "Id", plantillaCampo.FkCampo);
            ViewData["FkPlantilla"] = new SelectList(_context.Plantillas, "Id", "Id", plantillaCampo.FkPlantilla);
            return View(plantillaCampo);
        }

        // GET: PlantillaCampoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlantillaCampos == null)
            {
                return NotFound();
            }

            var plantillaCampo = await _context.PlantillaCampos.FindAsync(id);
            if (plantillaCampo == null)
            {
                return NotFound();
            }
            ViewData["FkCampo"] = new SelectList(_context.Campos, "Id", "Id", plantillaCampo.FkCampo);
            ViewData["FkPlantilla"] = new SelectList(_context.Plantillas, "Id", "Id", plantillaCampo.FkPlantilla);
            return View(plantillaCampo);
        }

        // POST: PlantillaCampoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FkCampo,FkPlantilla,OrdenCampo,EtiquetaCampo,Logituud,Requerido")] PlantillaCampo plantillaCampo)
        {
            if (id != plantillaCampo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantillaCampo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantillaCampoExists(plantillaCampo.Id))
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
            ViewData["FkCampo"] = new SelectList(_context.Campos, "Id", "Id", plantillaCampo.FkCampo);
            ViewData["FkPlantilla"] = new SelectList(_context.Plantillas, "Id", "Id", plantillaCampo.FkPlantilla);
            return View(plantillaCampo);
        }

        // GET: PlantillaCampoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlantillaCampos == null)
            {
                return NotFound();
            }

            var plantillaCampo = await _context.PlantillaCampos
                .Include(p => p.FkCampoNavigation)
                .Include(p => p.FkPlantillaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantillaCampo == null)
            {
                return NotFound();
            }

            return View(plantillaCampo);
        }

        // POST: PlantillaCampoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlantillaCampos == null)
            {
                return Problem("Entity set 'DBReferenciasContext.PlantillaCampos'  is null.");
            }
            var plantillaCampo = await _context.PlantillaCampos.FindAsync(id);
            if (plantillaCampo != null)
            {
                _context.PlantillaCampos.Remove(plantillaCampo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantillaCampoExists(int id)
        {
          return (_context.PlantillaCampos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
