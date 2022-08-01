using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            PerfilAnalista = new HashSet<PerfilAnalistum>();
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
        public virtual ICollection<PerfilAnalistum> PerfilAnalista { get; set; }
    }
}
