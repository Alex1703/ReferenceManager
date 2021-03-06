using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class RefFamiliar
    {
        public int Id { get; set; }
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
    }
}
