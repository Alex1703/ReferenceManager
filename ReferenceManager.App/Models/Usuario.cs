using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferenceManager.App.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            DetalleComunicacions = new HashSet<DetalleComunicacion>();
            GestionReferencia = new HashSet<GestionReferencium>();
            ListaReferencia = new HashSet<ListaReferencium>();
            Perfils = new HashSet<Perfil>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string Nombre { get; set; }
        public long Identificacion { get; set; }
        public byte[] Contrasena { get; set; }
        public byte[] ContrasenaKey { get; set; }
        public string EnLinea { get; set; }
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email is not valid")]
        public string Correo { get; set; }
        public DateTime? CambioContrasena { get; set; }
        [Required]
        [Display(Name = "Perfil")]
        public int? FkPerfil { get; set; }

        public virtual Perfil FkPerfilNavigation { get; set; }
        public virtual ICollection<DetalleComunicacion> DetalleComunicacions { get; set; }
        public virtual ICollection<GestionReferencium> GestionReferencia { get; set; }
        public virtual ICollection<ListaReferencium> ListaReferencia { get; set; }
        public virtual ICollection<Perfil> Perfils { get; set; }
    }
}
