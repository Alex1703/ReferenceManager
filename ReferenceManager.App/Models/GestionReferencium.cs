using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReferenceManager.App.Models
{
    [Table("GestionReferencia")]
    public partial class GestionReferencium
    {
        public int Id { get; set; }
        public int? FkListaReferencia { get; set; }
        public int? FkUsuario { get; set; }
        public string FullUrlRef { get; set; }
        public string Estado { get; set; }

        public virtual ListaReferencium FkListaReferenciaNavigation { get; set; }
        public virtual Usuario FkUsuarioNavigation { get; set; }
    }
}
