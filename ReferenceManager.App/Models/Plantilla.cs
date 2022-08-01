using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Plantilla
    {
        public Plantilla()
        {
            PlantillaCampos = new HashSet<PlantillaCampo>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<PlantillaCampo> PlantillaCampos { get; set; }
    }
}
