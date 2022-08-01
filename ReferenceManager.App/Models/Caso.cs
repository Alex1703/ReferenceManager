using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Caso
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public int? FkComercial { get; set; }
        public int? FkCliente { get; set; }

        public virtual Cliente FkClienteNavigation { get; set; }
        public virtual Comercial FkComercialNavigation { get; set; }
    }
}
