using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class TipoCliente
    {
        public TipoCliente()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
