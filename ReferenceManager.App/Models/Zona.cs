using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class Zona
    {
        public Zona()
        {
            Comercials = new HashSet<Comercial>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string? Nombre { get; set; }
        public string? Oficina { get; set; }
        public string? Ciudad { get; set; }

        public virtual ICollection<Comercial> Comercials { get; set; }
    }
}
