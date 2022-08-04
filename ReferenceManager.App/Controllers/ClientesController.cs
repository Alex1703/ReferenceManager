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
    public class ClientesController : Controller
    {
        private readonly DBReferenciasContext _context;
        private Int64 _identificacion;
        private string _tipoCliente;

        public ClientesController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var dBReferenciasContext = _context.Clientes.Include(c => c.FkTipoClienteNavigation);
            return View(await dBReferenciasContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.FkTipoClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["FkTipoCliente"] = new SelectList(_context.TipoClientes, "Id", "Nombre");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (cliente.FkCliente != null)
                {

                }
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create), "ListaReferenciums", new { idCliente = cliente.Id });
            }
            ViewData["FkTipoCliente"] = new SelectList(_context.TipoClientes, "Id", "Nombre", cliente.FkTipoCliente);
            return View();
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["FkTipoCliente"] = new SelectList(_context.TipoClientes, "Id", "Id", cliente.FkTipoCliente);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Activio,FkTipoCliente,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,TipoIdentificacion,NoIdentificacion,NombreNegocio,TipoNegocio,DireccionNegocio,TiempoOperacionAno,TiempoOperacionMese,TiempoEstablecimientoAno,TiempoEstablecimientoMes,TipoLocal,TipoVivienda,EstadoCivil,ClaseCiente")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            ViewData["FkTipoCliente"] = new SelectList(_context.TipoClientes, "Id", "Id", cliente.FkTipoCliente);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.FkTipoClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'DBReferenciasContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }

        [HttpPost]
        public IActionResult GetComercialById(string idComercial, string isTitular)
        {
            try
            {
                _identificacion = Convert.ToInt64(idComercial);
                _tipoCliente = isTitular;
                Comercial comercial = null;
                if (isTitular == "1")
                {
                    comercial = _context.Comercials.Include(x => x.FkZonaNavigation).FirstOrDefault(a => a.Cedula == idComercial);
                }
                else {
                    comercial = _context.Clientes.
                        Include(x => x.FkComercialNavigation).
                        Include(z=>z.FkComercialNavigation.FkZonaNavigation).
                        Where(x => x.FkCliente == null && x.NoIdentificacion == Convert.ToInt64(idComercial)).
                        Select(x => new Comercial {
                            Id = x.FkComercialNavigation.Id, 
                            Nombre = x.FkComercialNavigation.Nombre,
                            Cedula = x.FkComercialNavigation.Cedula,
                            FkZonaNavigation = x.FkComercialNavigation.FkZonaNavigation,
                            Clientes = x.FkComercialNavigation.Clientes
                        }).FirstOrDefault();
                    ViewData["IdCliente"] = comercial.Clientes.Where(x=>x.FkCliente == null).FirstOrDefault().Id;
                }
                
                if (comercial != null) 
                {
                    ViewData["identificacion"] = idComercial;
                    ViewData["isTitular"] = isTitular;
                    ViewData["Comercial"] = comercial;
                }
                else
                {
                    ViewData["Mensaje"] = "No se encontrol el usuario";
                }
                    

                ViewData["FkTipoCliente"] = new SelectList(_context.TipoClientes, "Id", "Nombre");
                
                return View("Create");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsTitular(Int64 idCliente)
        {
            try
            {
                var result = _context.Database.ExecuteSqlRaw("IsTitular @IdCliente", parameters: new[] { idCliente });
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
