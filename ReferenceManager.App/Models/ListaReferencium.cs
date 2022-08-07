using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferenceManager.App.Models
{
    public partial class ListaReferencium
    {
        public int Id { get; set; }
        public bool? Activio { get; set; }
        [Required]
        [Display(Name ="Persona de Contacto")]
        public string PersonaContacto { get; set; }
        [Required]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        [Required]
        [Display(Name = "Nombre del Cliente")]
        public int? FkCliente { get; set; }
        [Required]
        [Display(Name = "Tipo de Referencia")]
        public int? FkTipoReferencia { get; set; }
        public string Estado { get; set; }
        public int? FkPerfilAnalista { get; set; }

        public virtual Cliente FkClienteNavigation { get; set; }
        public virtual PerfilAnalistum FkPerfilAnalistaNavigation { get; set; }
        public virtual TipoReferencium FkTipoReferenciaNavigation { get; set; }

        
    }
}
