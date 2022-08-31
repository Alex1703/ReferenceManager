using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            DetalleComunicacions = new HashSet<DetalleComunicacion>();
            GestionReferencia = new HashSet<GestionReferencium>();
            ListaReferencia = new HashSet<ListaReferencium>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string Nombre { get; set; }
        public long Identificacion { get; set; }
        public byte[] Contrasena { get; set; }
        public byte[] ContrasenaKey { get; set; }
        public string EnLinea { get; set; }
        public string Correo { get; set; }
        public DateTime? CambioContrasena { get; set; }
        public int? FkPerfil { get; set; }

        public virtual Perfil FkPerfilNavigation { get; set; }
        public virtual ICollection<DetalleComunicacion> DetalleComunicacions { get; set; }
        public virtual ICollection<GestionReferencium> GestionReferencia { get; set; }
        public virtual ICollection<ListaReferencium> ListaReferencia { get; set; }
    }
}
