using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class ListaReferencium
    {
        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string PersonaContacto { get; set; }
        public string Telefono { get; set; }
        public int? FkCliente { get; set; }
        public int? FkTipoReferencia { get; set; }
        public int? FkTipoComunicacion { get; set; }

        public virtual Cliente FkClienteNavigation { get; set; }
        public virtual TipoComunicacion FkTipoComunicacionNavigation { get; set; }
        public virtual TipoReferencium FkTipoReferenciaNavigation { get; set; }
    }
}
