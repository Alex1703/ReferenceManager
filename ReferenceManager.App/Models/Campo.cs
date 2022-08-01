using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Campo
    {
        public Campo()
        {
            PlantillaCampos = new HashSet<PlantillaCampo>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string Nombre { get; set; }
        public int? FkTipoCampo { get; set; }

        public virtual TipoCampo FkTipoCampoNavigation { get; set; }
        public virtual ICollection<PlantillaCampo> PlantillaCampos { get; set; }
    }
}
