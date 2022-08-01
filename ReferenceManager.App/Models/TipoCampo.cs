using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class TipoCampo
    {
        public TipoCampo()
        {
            Campos = new HashSet<Campo>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string Nombre { get; set; }
        public string Descripcon { get; set; }

        public virtual ICollection<Campo> Campos { get; set; }
    }
}
