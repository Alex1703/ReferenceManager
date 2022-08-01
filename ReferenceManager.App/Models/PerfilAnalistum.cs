using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class PerfilAnalistum
    {
        public PerfilAnalistum()
        {
            TipoComunicacions = new HashSet<TipoComunicacion>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public bool? Activio { get; set; }
        public int? FkUsuario { get; set; }

        public virtual Usuario FkUsuarioNavigation { get; set; }
        public virtual ICollection<TipoComunicacion> TipoComunicacions { get; set; }
    }
}
