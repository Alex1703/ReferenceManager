using System;
using System.Collections.Generic;

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
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string TipoIdentificacion { get; set; }
        public long? NoIdentificacion { get; set; }
        public string NombreNegocio { get; set; }
        public string TipoNegocio { get; set; }
        public string DireccionNegocio { get; set; }
        public int? TiempoOperacionAno { get; set; }
        public int? TiempoOperacionMese { get; set; }
        public int? TiempoEstablecimientoAno { get; set; }
        public int? TiempoEstablecimientoMes { get; set; }
        public string TipoLocal { get; set; }
        public string TipoVivienda { get; set; }
        public string EstadoCivil { get; set; }
        public string ClaseCiente { get; set; }
        public int? FkCliente { get; set; }

        public virtual Cliente FkClienteNavigation { get; set; }
        public virtual TipoCliente FkTipoClienteNavigation { get; set; }
        public virtual ICollection<Caso> Casos { get; set; }
        public virtual ICollection<Cliente> InverseFkClienteNavigation { get; set; }
        public virtual ICollection<ListaReferencium> ListaReferencia { get; set; }

        public string FullName { get { return (PrimerNombre + " " + SegundoNombre + " " + PrimerApellido + " " + SegundoApellido); } }
    }
}
