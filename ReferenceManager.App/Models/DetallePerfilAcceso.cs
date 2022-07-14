using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class DetallePerfilAcceso
    {
        public int Id { get; set; }
        public bool? Activio { get; set; }
        public int? FkPerfil { get; set; }
        public int? FkAcceso { get; set; }

        public virtual Acceso? FkAccesoNavigation { get; set; }
        public virtual Perfil? FkPerfilNavigation { get; set; }
    }
}
