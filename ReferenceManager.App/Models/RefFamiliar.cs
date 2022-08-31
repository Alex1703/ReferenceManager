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
        public string ConfirmacionNombre { get; set; }
        public string Parentezco { get; set; }
        public string EstadoCivil { get; set; }
        public string NombreConyuge { get; set; }
        public string CantidadHijos { get; set; }
        public string QuienVive { get; set; }
        public string Actividad { get; set; }
        public string TiempoNegocio { get; set; }
        public string BarrioNegocio { get; set; }
        public string CantidadEmpleados { get; set; }
        public string DireccionTelefono { get; set; }
        public string Concepto { get; set; }

        public virtual ICollection<DetalleComunicacion> DetalleComunicacions { get; set; }
    }
}
