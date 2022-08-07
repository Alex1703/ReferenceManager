using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class RefProveedor
    {
        public RefProveedor()
        {
            DetalleComunicacions = new HashSet<DetalleComunicacion>();
        }

        public int Id { get; set; }
        public string ConfirmarNombre { get; set; }
        public string Cargo { get; set; }
        public string DireccionProveedor { get; set; }
        public string TelefonoProveedor { get; set; }
        public string TiempoVenta { get; set; }
        public string ProductoVenta { get; set; }
        public string FrecuenciaCompra { get; set; }
        public string PromedioCompra { get; set; }
        public string ValorUltimaCompra { get; set; }
        public string ContadoCredito { get; set; }
        public string PlazoCredito { get; set; }
        public string CupoCredito { get; set; }
        public string PagoCredito { get; set; }
        public string Concepto { get; set; }

        public virtual ICollection<DetalleComunicacion> DetalleComunicacions { get; set; }
    }
}
