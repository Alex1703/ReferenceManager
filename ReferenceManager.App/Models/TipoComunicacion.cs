﻿using System;
using System.Collections.Generic;

namespace ReferenceManager.App.Models
{
    public partial class TipoComunicacion
    {
        public TipoComunicacion()
        {
            Referencia = new HashSet<Referencium>();
        }

        public int Id { get; set; }
        public bool? Activio { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? FkPerfilAnalista { get; set; }

        public virtual PerfilAnalistum? FkPerfilAnalistaNavigation { get; set; }
        public virtual ICollection<Referencium> Referencia { get; set; }
    }
}
