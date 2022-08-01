using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class PlantillaCampo
    {
        public PlantillaCampo()
        {
            DetallePlantillaCampos = new HashSet<DetallePlantillaCampo>();
        }

        public int Id { get; set; }
        public int? FkCampo { get; set; }
        public int? FkPlantilla { get; set; }
        public int? OrdenCampo { get; set; }
        public string EtiquetaCampo { get; set; }
        public int? Logituud { get; set; }
        public bool? Requerido { get; set; }

        public virtual Campo FkCampoNavigation { get; set; }
        public virtual Plantilla FkPlantillaNavigation { get; set; }
        public virtual ICollection<DetallePlantillaCampo> DetallePlantillaCampos { get; set; }
    }
}
