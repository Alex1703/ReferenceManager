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
    public class CampoesController : Controller
    {
        private readonly DBReferenciasContext _context;

        public CampoesController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: Campoes
        public async Task<IActionResult> Index()
        {
            var dBReferenciasContext = _context.Campos.Include(c => c.FkTipoCampoNavigation);
            return View(await dBReferenciasContext.ToListAsync());
        }

        // GET: Campoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Campos == null)
            {
                return NotFound();
            }

            var campo = await _context.Campos
                .Include(c => c.FkTipoCampoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campo == null)
            {
                return NotFound();
            }

            return View(campo);
        }

        // GET: Campoes/Create
        public IActionResult Create()
        {
            ViewData["FkTipoCampo"] = new SelectList(_context.TipoCampos, "Id", "Nombre");
            return View();
        }

        // POST: Campoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Activio,Nombre,FkTipoCampo")] Campo campo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkTipoCampo"] = new SelectList(_context.TipoCampos, "Id", "Id", campo.FkTipoCampo);
            return View(campo);
        }

        // GET: Campoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Campos == null)
            {
                return NotFound();
            }

            var campo = await _context.Campos.FindAsync(id);
            if (campo == null)
            {
                return NotFound();
            }
            ViewData["FkTipoCampo"] = new SelectList(_context.TipoCampos, "Id", "Id", campo.FkTipoCampo);
            return View(campo);
        }

        // POST: Campoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Activio,Nombre,FkTipoCampo")] Campo campo)
        {
            if (id != campo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampoExists(campo.Id))
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
            ViewData["FkTipoCampo"] = new SelectList(_context.TipoCampos, "Id", "Id", campo.FkTipoCampo);
            return View(campo);
        }

        // GET: Campoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Campos == null)
            {
                return NotFound();
            }

            var campo = await _context.Campos
                .Include(c => c.FkTipoCampoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campo == null)
            {
                return NotFound();
            }

            return View(campo);
        }

        // POST: Campoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Campos == null)
            {
                return Problem("Entity set 'DBReferenciasContext.Campos'  is null.");
            }
            var campo = await _context.Campos.FindAsync(id);
            if (campo != null)
            {
                _context.Campos.Remove(campo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampoExists(int id)
        {
          return (_context.Campos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
