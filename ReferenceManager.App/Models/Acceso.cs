using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Acceso
    {
        public Acceso()
        {
            DetallePerfilAccesos = new HashSet<DetallePerfilAcceso>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string Modulo { get; set; }
        public string Url { get; set; }

        public virtual ICollection<DetallePerfilAcceso> DetallePerfilAccesos { get; set; }
    }
}
