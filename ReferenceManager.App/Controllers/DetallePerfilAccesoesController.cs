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
    public class DetallePerfilAccesoesController : Controller
    {
        private readonly DBReferenciasContext _context;

        public DetallePerfilAccesoesController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: DetallePerfilAccesoes
        public async Task<IActionResult> Index()
        {
            var dBReferenciasContext = _context.DetallePerfilAccesos.Include(d => d.FkAccesoNavigation).Include(d => d.FkPerfilNavigation);
            return View(await dBReferenciasContext.ToListAsync());
        }

        // GET: DetallePerfilAccesoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetallePerfilAccesos == null)
            {
                return NotFound();
            }

            var detallePerfilAcceso = await _context.DetallePerfilAccesos
                .Include(d => d.FkAccesoNavigation)
                .Include(d => d.FkPerfilNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallePerfilAcceso == null)
            {
                return NotFound();
            }

            return View(detallePerfilAcceso);
        }

        // GET: DetallePerfilAccesoes/Create
        public IActionResult Create()
        {
            ViewData["FkAcceso"] = new SelectList(_context.Accesos, "Id", "Id");
            ViewData["FkPerfil"] = new SelectList(_context.Perfils, "Id", "Id");
            return View();
        }

        // POST: DetallePerfilAccesoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Activio,FkPerfil,FkAcceso")] DetallePerfilAcceso detallePerfilAcceso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallePerfilAcceso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkAcceso"] = new SelectList(_context.Accesos, "Id", "Id", detallePerfilAcceso.FkAcceso);
            ViewData["FkPerfil"] = new SelectList(_context.Perfils, "Id", "Id", detallePerfilAcceso.FkPerfil);
            return View(detallePerfilAcceso);
        }

        // GET: DetallePerfilAccesoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetallePerfilAccesos == null)
            {
                return NotFound();
            }

            var detallePerfilAcceso = await _context.DetallePerfilAccesos.FindAsync(id);
            if (detallePerfilAcceso == null)
            {
                return NotFound();
            }
            ViewData["FkAcceso"] = new SelectList(_context.Accesos, "Id", "Id", detallePerfilAcceso.FkAcceso);
            ViewData["FkPerfil"] = new SelectList(_context.Perfils, "Id", "Id", detallePerfilAcceso.FkPerfil);
            return View(detallePerfilAcceso);
        }

        // POST: DetallePerfilAccesoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Activio,FkPerfil,FkAcceso")] DetallePerfilAcceso detallePerfilAcceso)
        {
            if (id != detallePerfilAcceso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallePerfilAcceso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallePerfilAccesoExists(detallePerfilAcceso.Id))
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
            ViewData["FkAcceso"] = new SelectList(_context.Accesos, "Id", "Id", detallePerfilAcceso.FkAcceso);
            ViewData["FkPerfil"] = new SelectList(_context.Perfils, "Id", "Id", detallePerfilAcceso.FkPerfil);
            return View(detallePerfilAcceso);
        }

        // GET: DetallePerfilAccesoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetallePerfilAccesos == null)
            {
                return NotFound();
            }

            var detallePerfilAcceso = await _context.DetallePerfilAccesos
                .Include(d => d.FkAccesoNavigation)
                .Include(d => d.FkPerfilNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallePerfilAcceso == null)
            {
                return NotFound();
            }

            return View(detallePerfilAcceso);
        }

        // POST: DetallePerfilAccesoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetallePerfilAccesos == null)
            {
                return Problem("Entity set 'DBReferenciasContext.DetallePerfilAccesos'  is null.");
            }
            var detallePerfilAcceso = await _context.DetallePerfilAccesos.FindAsync(id);
            if (detallePerfilAcceso != null)
            {
                _context.DetallePerfilAccesos.Remove(detallePerfilAcceso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallePerfilAccesoExists(int id)
        {
          return (_context.DetallePerfilAccesos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
