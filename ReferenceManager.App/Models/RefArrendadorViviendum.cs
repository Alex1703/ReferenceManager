using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferenceManager.App.Models
{
    public partial class RefArrendadorViviendum
    {
        public RefArrendadorViviendum()
        {
            DetalleComunicacions = new HashSet<DetalleComunicacion>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "¿Qué le arrienda al señor (a)?")]
        public string QueArrienda { get; set; }
        [Required]
        [Display(Name = "¿Con quién vive él (ella)?")]
        public string QuienVive { get; set; }
        [Required]
        [Display(Name = "¿Qué estado civil tiene el señor (a)?")]
        public string EstadoCivil { get; set; }
        [Required]
        [Display(Name = "¿Cómo se llama el/la esposo (a) de su inquilino?")]
        public string NombreConyuge { get; set; }
        [Required]
        [Display(Name = "¿A qué se dedica su inquilino?")]
        public string Actividad { get; set; }
        [Required]
        [Display(Name = "¿Hace cuánto le tiene arrendado?")]
        public string TiempoArriendo { get; set; }
        [Required]
        [Display(Name = "¿Cuál es la dirección de esa vivienda?")]
        public string DireccionVivienda { get; set; }
        [Required]
        [Display(Name = "¿Cuánto es el canon del arriendo? ")]
        public decimal CanonArriendo { get; set; }
        [Required]
        [Display(Name = "¿Incluye servicios el canon de arriendo o son aparte?")]
        public string IncluyeServicios { get; set; }
        [Required]
        [Display(Name = "¿Ha sido puntual y responsable con el pago del arriendo?")]
        public string PuntualResponsable { get; set; }
        [Required]
        [Display(Name = "¿Qué concepto tiene usted del señor (a)?")]
        public string Concepto { get; set; }

        public virtual ICollection<DetalleComunicacion> DetalleComunicacions { get; set; }
    }
}
