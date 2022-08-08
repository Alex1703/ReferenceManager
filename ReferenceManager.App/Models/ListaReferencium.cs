using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferenceManager.App.Models
{
    public partial class ListaReferencium
    {
        public ListaReferencium()
        {
            DetalleComunicacions = new HashSet<DetalleComunicacion>();
            GestionReferencia = new HashSet<GestionReferencium>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        [Required]
        [Display(Name = "Persona de Contacto")]
        public string PersonaContacto { get; set; }
        [Required]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        [Required]
        [Display(Name = "Nombre de Cliente")]
        public int? FkCliente { get; set; }
        [Required]
        [Display(Name = "Tipo de Referencia")]
        public int? FkTipoReferencia { get; set; }
        public string Estado { get; set; }
        public int? FkUsuario { get; set; }

        public virtual Cliente FkClienteNavigation { get; set; }
        public virtual TipoReferencium FkTipoReferenciaNavigation { get; set; }
        public virtual Usuario FkUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleComunicacion> DetalleComunicacions { get; set; }
        public virtual ICollection<GestionReferencium> GestionReferencia { get; set; }

        
    }
}
