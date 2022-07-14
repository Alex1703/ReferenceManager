using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class TipoReferencium
    {
        public TipoReferencium()
        {
            DetallePlantillaCampos = new HashSet<DetallePlantillaCampo>();
            Referencia = new HashSet<Referencium>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<DetallePlantillaCampo> DetallePlantillaCampos { get; set; }
        public virtual ICollection<Referencium> Referencia { get; set; }
    }
}
