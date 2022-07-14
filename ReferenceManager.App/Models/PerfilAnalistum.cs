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
        public string? Codigo { get; set; }
        public bool? Activio { get; set; }
        public int? FkPerfil { get; set; }

        public virtual Perfil? FkPerfilNavigation { get; set; }
        public virtual ICollection<TipoComunicacion> TipoComunicacions { get; set; }
    }
}
