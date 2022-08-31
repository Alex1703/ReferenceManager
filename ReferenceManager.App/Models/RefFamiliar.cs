using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferenceManager.App.Models
{
    public partial class RefFamiliar
    {
        public RefFamiliar()
        {
            DetalleComunicacions = new HashSet<DetalleComunicacion>();
        }

        public int Id { get; set; }

        [Required]
        [Display (Name = "Me confirma su nombre completo, por favor")]
        public string ConfirmacionNombre { get; set; }
        [Required]
        [Display(Name = "¿Qué parentesco tiene con el señor (a)?")]
        public string Parentezco { get; set; }
        [Required]
        [Display(Name = "¿Qué estado civil tiene el señor (a)?")]
        public string EstadoCivil { get; set; }
        [Required]
        [Display(Name = "¿Cómo se llama el cónyuge de él (ella)?")]
        public string NombreConyuge { get; set; }
        [Required]
        [Display(Name = "¿Cuántos hijos tiene él (ella)?")]
        public string CantidadHijos { get; set; }
        [Required]
        [Display(Name = "¿Con quién vive él (ella)?")]
        public string QuienVive { get; set; }
        [Required]
        [Display(Name = "¿A qué se dedica su familiar?")]
        public string Actividad { get; set; }
        [Required]
        [Display(Name = "¿Cuánto tiempo lleva con el negocio?")]
        public string TiempoNegocio { get; set; }
        [Required]
        [Display(Name = "¿En qué barrio o sector tiene el negocio?")]
        public string BarrioNegocio { get; set; }
        [Required]
        [Display(Name = "¿Cuántos empleados tiene en el negocio?")]
        public string CantidadEmpleados { get; set; }
        [Required]
        [Display(Name = "¿Cuál es su dirección y teléfono?")]
        public string DireccionTelefono { get; set; }
        [Required]
        [Display(Name = "¿Qué concepto tiene usted del señor (a)?")]
        public string Concepto { get; set; }

        public virtual ICollection<DetalleComunicacion> DetalleComunicacions { get; set; }
    }
}
