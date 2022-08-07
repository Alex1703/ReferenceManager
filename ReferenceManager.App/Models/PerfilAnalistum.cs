using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class PerfilAnalistum
    {
        public PerfilAnalistum()
        {
            DetalleComunicacions = new HashSet<DetalleComunicacion>();
            ListaReferencia = new HashSet<ListaReferencium>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public bool? Activio { get; set; }
        public int? FkUsuario { get; set; }

        public virtual Usuario FkUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleComunicacion> DetalleComunicacions { get; set; }
        public virtual ICollection<ListaReferencium> ListaReferencia { get; set; }
    }
}
