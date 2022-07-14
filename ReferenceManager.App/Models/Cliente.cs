using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Referencia = new HashSet<Referencium>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public int? FkComercial { get; set; }
        public int? FkTipoCliente { get; set; }

        public virtual Comercial? FkComercialNavigation { get; set; }
        public virtual TipoCliente? FkTipoClienteNavigation { get; set; }
        public virtual ICollection<Referencium> Referencia { get; set; }
    }
}
