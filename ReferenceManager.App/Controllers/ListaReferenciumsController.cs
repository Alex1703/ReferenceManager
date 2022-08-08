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
    public class ListaReferenciumsController : Controller
    {
        private readonly DBReferenciasContext _context;
        private int _idCliente;
        public ListaReferenciumsController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: ListaReferenciums
        public async Task<IActionResult> Index()
        {

            var dBReferenciasContext = _context.ListaReferencia.Include(l => l.FkClienteNavigation).Include(l => l.FkUsuarioNavigation).Include(l => l.FkTipoReferenciaNavigation).Where(x => x.FkCliente == (int)TempData["idCliente"]);
            return View(await dBReferenciasContext.ToListAsync());
        }

        // GET: ListaReferenciums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ListaReferencia == null)
            {
                return NotFound();
            }

            var listaReferencium = await _context.ListaReferencia
                .Include(l => l.FkClienteNavigation)
                .Include(l => l.FkUsuarioNavigation)
                .Include(l => l.FkTipoReferenciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaReferencium == null)
            {
                return NotFound();
            }

            return View(listaReferencium);
        }

        // GET: ListaReferenciums/Create
        public IActionResult Create(string idCliente)
        {
            TempData["idCliente"] = Convert.ToInt32(idCliente);
            ViewData["FkCliente"] = new SelectList(_context.Clientes
                .Where(x => x.Id == Convert.ToInt32(idCliente))
                .Select(x => new { Id = x.Id, Nombre = x.PrimerNombre + " " + x.SegundoNombre + " " + x.PrimerApellido + " " + x.SegundoApellido }), "Id", "Nombre");
            ViewData["FkUsuario"] = new SelectList(_context.Usuarios, "Id", "Id");
            ViewData["FkTipoReferencia"] = new SelectList(_context.TipoReferencia, "Id", "Nombre");
            var referencias = _context.ListaReferencia
                .Include(l => l.FkClienteNavigation)
                .Include(l => l.FkUsuarioNavigation)
                .Include(l => l.FkTipoReferenciaNavigation)
                .Include(l => l.FkUsuarioNavigation)
                .Where(x => x.FkCliente == (int)TempData["idCliente"]).ToList();

            ViewData["ListaReferencia"] = referencias.Count > 0 ? referencias : null;

            return View();
        }

        // POST: ListaReferenciums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Activio,PersonaContacto,Telefono,FkCliente,FkTipoReferencia,Estado,FkPerfilAnalista")] ListaReferencium listaReferencium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listaReferencium);
                await _context.SaveChangesAsync();
            }
            ViewData["FkCliente"] = new SelectList(_context.Clientes.Select(x => new { Id = x.Id, Nombre = x.PrimerNombre + " " + x.SegundoNombre + " " + x.PrimerApellido + " " + x.SegundoApellido }), "Id", "Nombre");
            ViewData["FkUsuarios"] = new SelectList(_context.Usuarios, "Id", "Id");
            ViewData["FkTipoReferencia"] = new SelectList(_context.TipoReferencia, "Id", "Nombre");
            ViewData["ListaReferencia"] = _context.ListaReferencia
                .Include(l => l.FkClienteNavigation)
                .Include(l => l.FkUsuarioNavigation)
                .Include(l => l.FkTipoReferenciaNavigation)
                .Where(x => x.FkCliente == (int)TempData["idCliente"]);
            return View();
        }

        // GET: ListaReferenciums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ListaReferencia == null)
            {
                return NotFound();
            }

            var listaReferencium = await _context.ListaReferencia.FindAsync(id);
            if (listaReferencium == null)
            {
                return NotFound();
            }
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "Id", "Id", listaReferencium.FkCliente);
            ViewData["FkTipoReferencia"] = new SelectList(_context.TipoReferencia, "Id", "Id", listaReferencium.FkTipoReferencia);
            return View(listaReferencium);
        }

        // POST: ListaReferenciums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Activio,PersonaContacto,Telefono,FkCliente,FkTipoReferencia,Estado,FkPerfilAnalista")] ListaReferencium listaReferencium)
        {
            if (id != listaReferencium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listaReferencium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaReferenciumExists(listaReferencium.Id))
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
            ViewData["FkCliente"] = new SelectList(_context.Clientes, "Id", "Id", listaReferencium.FkCliente);
            ViewData["FkTipoReferencia"] = new SelectList(_context.TipoReferencia, "Id", "Id", listaReferencium.FkTipoReferencia);
            return View(listaReferencium);
        }

        // GET: ListaReferenciums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ListaReferencia == null)
            {
                return NotFound();
            }

            var listaReferencium = await _context.ListaReferencia
                .Include(l => l.FkClienteNavigation)
                .Include(l => l.FkUsuarioNavigation)
                .Include(l => l.FkTipoReferenciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaReferencium == null)
            {
                return NotFound();
            }

            return View(listaReferencium);
        }

        // POST: ListaReferenciums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ListaReferencia == null)
            {
                return Problem("Entity set 'DBReferenciasContext.ListaReferencia'  is null.");
            }
            var listaReferencium = await _context.ListaReferencia.FindAsync(id);
            if (listaReferencium != null)
            {
                _context.ListaReferencia.Remove(listaReferencium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaReferenciumExists(int id)
        {
            return _context.ListaReferencia.Any(e => e.Id == id);
        }
    }
}
