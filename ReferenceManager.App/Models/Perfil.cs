using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Perfil
    {
        public Perfil()
        {
            DetallePerfilAccesos = new HashSet<DetallePerfilAcceso>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Activio { get; set; }

        public virtual ICollection<DetallePerfilAcceso> DetallePerfilAccesos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
