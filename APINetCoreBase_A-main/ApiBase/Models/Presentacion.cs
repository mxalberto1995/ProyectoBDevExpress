using System;
using System.Collections.Generic;

namespace DevExtremePB_Api.Models
{
    public partial class Presentacion
    {
        public Presentacion()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int Idpresentacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}
