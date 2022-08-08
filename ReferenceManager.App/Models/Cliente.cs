using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReferenceManager.App.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Casos = new HashSet<Caso>();
            InverseFkClienteNavigation = new HashSet<Cliente>();
            ListaReferencia = new HashSet<ListaReferencium>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public int? FkTipoCliente { get; set; }
        [Required]
        [Display(Name = "Primer Nombre")]
        public string PrimerNombre { get; set; }

        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }
        [Required]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }
        [Required]
        [Display(Name = "Tipo de Identificacion")]
        public string TipoIdentificacion { get; set; }
        [Required]
        [Display(Name = "Numero de Identificacion")]
        public long? NoIdentificacion { get; set; }
        [Required]
        [Display(Name = "Nombre del Negocio")]
        public string NombreNegocio { get; set; }
        [Required]
        [Display(Name = "Tipo de Negocio")]
        public string TipoNegocio { get; set; }
        [Required]
        [Display(Name = "Direccion de Negocio")]
        public string DireccionNegocio { get; set; }
        [Required]
        [Display(Name = "Tiempo de Operacion (Años)")]
        public int? TiempoOperacionAno { get; set; }
        [Required]
        [Display(Name = "Tiempo de Operacion (Meses)")]
        public int? TiempoOperacionMese { get; set; }
        [Required]
        [Display(Name = "Tiempo de Operacion en el Establecimiento (Años)")]
        public int? TiempoEstablecimientoAno { get; set; }
        [Required]
        [Display(Name = "Tiempo de Operacion en el Establecimiento (Meses)")]
        public int? TiempoEstablecimientoMes { get; set; }
        [Required]
        [Display(Name = "Tipo de Local")]
        public string TipoLocal { get; set; }
        [Required]
        [Display(Name = "Tipo de Vivienda")]
        public string TipoVivienda { get; set; }
        [Required]
        [Display(Name = "Estado Civil")]
        public string EstadoCivil { get; set; }
        [Required]
        [Display(Name = "Clase de Cliente")]
        public string ClaseCiente { get; set; }
        public int? FkCliente { get; set; }

        public virtual Cliente FkClienteNavigation { get; set; }
        public virtual TipoCliente FkTipoClienteNavigation { get; set; }
        public virtual ICollection<Caso> Casos { get; set; }
        public virtual ICollection<Cliente> InverseFkClienteNavigation { get; set; }
        public virtual ICollection<ListaReferencium> ListaReferencia { get; set; }

        public string FullName
        {
            get
            {
                return (PrimerNombre+" "+SegundoNombre+" "+PrimerApellido+" "+SegundoApellido);
            }
        }
    }
}
