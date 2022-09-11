using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Models;
using ReferenceManager.App.Models.Enum;
using System.Runtime.CompilerServices;

namespace ReferenceManager.App.Controllers
{
    public class ReportesController : Controller
    {
        private readonly DBReferenciasContext _context;
        public ReportesController(DBReferenciasContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public IActionResult ConsultarCliente()
        {

            return View();
        }

        public IActionResult BuscarEstadoReferenciasCliente(string idCliente)
        {
            TempData["idCliente"] = Convert.ToInt32(idCliente);
            var fkCliente = new SelectList(_context.Clientes
                .Where(x => x.Id == Convert.ToInt32(idCliente))
                .Select(x => new { Id = x.Id, Nombre = x.FullName }), "Id", "Nombre");
            ViewData["FkUsuario"] = new SelectList(_context.Usuarios, "Id", "Id");
            ViewData["FkTipoReferencia"] = new SelectList(_context.TipoReferencia, "Id", "Nombre");
            var referencias = _context.ListaReferencia
                .Include(l => l.FkClienteNavigation)
                .Include(l => l.FkUsuarioNavigation)
                .Include(l => l.FkTipoReferenciaNavigation)
                .Where(x => x.FkCliente == Convert.ToInt32(idCliente) && x.Activio == true).ToList();

            ViewData["ListaReferencia"] = referencias.Count > 0 ? referencias : null;
            ViewData["FkCliente"] = fkCliente != null ? fkCliente : null;

            return View();
        }
        //Obtener Caso X Asesor
        [HttpPost]
        public ActionResult FindCasosXAsesorAbiertos(string identificacion)
        {
            var ListCasos = _context.Casos.Include(x => x.FkComercialNavigation).Where(x => x.FkComercialNavigation.Cedula == identificacion && x.Estado == EstadoSolicitud.Abierto.ToString()).ToList();
            return Json(ListCasos);
        }

        //Obtener Todos los Caso X Asesor 
        [HttpPost]
        public ActionResult FindCasosXAsesor(string identificacion)
        {
            var ListCasos = _context.Casos.Include(x => x.FkComercialNavigation).Where(x => x.FkComercialNavigation.Cedula == identificacion).ToList();
            return Json(ListCasos);
        }

        //Obtener Referencias por Cliente Titular
        public ActionResult BuscarCliente(string identificacion)
        {
            Caso caso = null;
            try
            {
                caso = _context.Casos
                      .Include(x => x.FkClienteNavigation)
                      .Include(x => x.FkClienteNavigation.FkClienteNavigation)
                      .Include(j => j.FkComercialNavigation)
                      .Include(z => z.FkComercialNavigation.FkZonaNavigation)
                      .Include(x => x.FkClienteNavigation.ListaReferencia).ThenInclude(y => y.FkTipoReferenciaNavigation)
                      .Include(x => x.FkClienteNavigation.ListaReferencia).ThenInclude(y => y.FkUsuarioNavigation)
                      .Include(x => x.FkClienteNavigation.ListaReferencia).ThenInclude(y => y.DetalleComunicacions).ThenInclude(y => y.FkRefFamiliarNavigation)
                      .Include(x => x.FkClienteNavigation.ListaReferencia).ThenInclude(y => y.DetalleComunicacions).ThenInclude(y => y.FkRefProveedorNavigation)
                      .Include(x => x.FkClienteNavigation.ListaReferencia).ThenInclude(y => y.DetalleComunicacions).ThenInclude(y => y.FkRefArrendadorViviendaNavigation)
                      .Include(x => x.FkClienteNavigation.ListaReferencia).ThenInclude(y => y.DetalleComunicacions).ThenInclude(y => y.FkRefArrendadorLocalNavigation)
                      .Where(x => x.Estado == EstadoSolicitud.Abierto.ToString() && x.FkClienteNavigation.NoIdentificacion == Convert.ToInt64(identificacion) && x.FkClienteNavigation.FkCliente == null).FirstOrDefault();
                return Json(caso);
            }
            catch (Exception)
            {
                return Json(caso);
            }
          
        }

        public ActionResult BuscarCoodeudor(string identificacion)
        {
            Cliente cliente = null;
            try
            {
                
                var idTitular = _context.Clientes.FirstOrDefault(x => x.NoIdentificacion == Convert.ToInt64(identificacion));
                if (idTitular != null)
                {
                    cliente = _context.Clientes
                            .Include(x => x.ListaReferencia)
                            .Include(x => x.ListaReferencia).ThenInclude(y => y.FkTipoReferenciaNavigation)
                            .Include(x => x.ListaReferencia).ThenInclude(y => y.FkUsuarioNavigation)
                            .Include(x => x.ListaReferencia).ThenInclude(y => y.DetalleComunicacions).ThenInclude(y => y.FkRefFamiliarNavigation)
                            .Include(x => x.ListaReferencia).ThenInclude(y => y.DetalleComunicacions).ThenInclude(y => y.FkRefProveedorNavigation)
                            .Include(x => x.ListaReferencia).ThenInclude(y => y.DetalleComunicacions).ThenInclude(y => y.FkRefArrendadorViviendaNavigation)
                            .Include(x => x.ListaReferencia).ThenInclude(y => y.DetalleComunicacions).ThenInclude(y => y.FkRefArrendadorLocalNavigation)
                            .Where(x => x.FkCliente == idTitular.Id && x.Activio == true).FirstOrDefault();
                }

                return Json(cliente);

            }
            catch (Exception)
            {
                return Json(cliente);
            }

        }

        public async Task<IActionResult> FindClient(string identificacion)
        {
            var Cliente = _context.Clientes.FirstOrDefault(x => x.Activio == true && x.NoIdentificacion == Convert.ToInt64(identificacion));

            if (Cliente != null)
            {
                return RedirectToAction(nameof(BuscarEstadoReferenciasCliente), new { idCliente = Cliente.Id });
            }
            TempData["Msg"] = "No se encontro el cliente";
            return RedirectToAction(nameof(Index));
        }
    }
}
