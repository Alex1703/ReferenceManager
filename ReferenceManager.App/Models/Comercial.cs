using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Comercial
    {
        public Comercial()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string? Nombre { get; set; }
        public string? Cedula { get; set; }
        public int? FkZona { get; set; }

        public virtual Zona? FkZonaNavigation { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
