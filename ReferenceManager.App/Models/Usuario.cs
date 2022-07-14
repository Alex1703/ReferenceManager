using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string? Nombre { get; set; }
        public long? Identificacion { get; set; }
        public string? Contrasena { get; set; }
        public DateTime? CambioContrasena { get; set; }
        public int? FkPerfil { get; set; }

        public virtual Perfil? FkPerfilNavigation { get; set; }
    }
}
