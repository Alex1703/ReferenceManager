using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class DetallePlantillaCampo
    {
        public int Id { get; set; }
        public int? FkTipoCliente { get; set; }
        public int? FkTipoReferencia { get; set; }
        public int? FkPlantillaCampos { get; set; }
        public string? NombrePlantilla { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsCliente { get; set; }
        public bool? Activo { get; set; }

        public virtual PlantillaCampo? FkPlantillaCamposNavigation { get; set; }
        public virtual TipoCliente? FkTipoClienteNavigation { get; set; }
        public virtual TipoReferencium? FkTipoReferenciaNavigation { get; set; }
    }
}
