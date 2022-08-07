using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class DetalleComunicacion
    {
        public int Id { get; set; }
        public bool? Activo { get; set; }
        public string Descripcion { get; set; }
        public int? FkPerfilAnalista { get; set; }
        public int? FkRefArrendadorLocal { get; set; }
        public int? FkRefArrendadorVivienda { get; set; }
        public int? FkRefFamiliar { get; set; }
        public int? FkRefProveedor { get; set; }

        public virtual PerfilAnalistum FkPerfilAnalistaNavigation { get; set; }
        public virtual RefArrendadorLocal FkRefArrendadorLocalNavigation { get; set; }
        public virtual RefArrendadorViviendum FkRefArrendadorViviendaNavigation { get; set; }
        public virtual RefFamiliar FkRefFamiliarNavigation { get; set; }
        public virtual RefProveedor FkRefProveedorNavigation { get; set; }
    }
}
