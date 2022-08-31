using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferenceManager.App.Models
{
    public partial class RefProveedor
    {
        public RefProveedor()
        {
            DetalleComunicacions = new HashSet<DetalleComunicacion>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Me confirma su nombre completo, por favor")]
        public string ConfirmarNombre { get; set; }
        [Required]
        [Display(Name = "¿Cuál es su cargo?")]
        public string Cargo { get; set; }
        [Required]
        [Display(Name = "¿Cuál es la dirección de su negocio/empresa?")]
        public string DireccionProveedor { get; set; }
        [Required]
        [Display(Name = "Me confirma el teléfono fijo de su negocio o empresa")]
        public string TelefonoProveedor { get; set; }
        [Required]
        [Display(Name = "¿Cuánto tiempo lleva vendiéndole al señor (a)? ")]
        public string TiempoVenta { get; set; }
        [Required]
        [Display(Name = "¿Qué productos le vende Ud. a él (a)?")]
        public string ProductoVenta { get; set; }
        [Required]
        [Display(Name = "¿Le Compra Diario -Semanal -Quincenal -Mensual? ")]
        public string FrecuenciaCompra { get; set; }
        [Required]
        [Display(Name = "¿Cuánto es el promedio en dinero de compra?")]
        public string PromedioCompra { get; set; }
        [Required]
        [Display(Name = "¿De cuánto fue la última compra?")]
        public string ValorUltimaCompra { get; set; }
        [Required]
        [Display(Name = "¿Le vende de contado o a crédito?")]
        public string ContadoCredito { get; set; }
        [Required]
        [Display(Name = "¿Qué plazo le da para pagar el crédito?")]
        public string PlazoCredito { get; set; }
        [Required]
        [Display(Name = "¿De cuánto es el cupo de crédito?")]
        public string CupoCredito { get; set; }
        [Required]
        [Display(Name = "¿Le cancela en cheque, efectivo o tarjeta?")]
        public string PagoCredito { get; set; }
        [Required]
        [Display(Name = "¿Qué concepto tiene usted del señor (a)?")]
        public string Concepto { get; set; }
     

        public virtual ICollection<DetalleComunicacion> DetalleComunicacions { get; set; }
    }
}
