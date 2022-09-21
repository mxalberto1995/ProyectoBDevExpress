using System;
using System.Collections.Generic;

namespace DevExtremePB_Api.Models
{
    public partial class Articulo
    {
        public Articulo()
        {
            DetalleIngresos = new HashSet<DetalleIngreso>();
        }

        public int Idarticulo { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }
        public int Idcategoria { get; set; }
        public int Idpresentacion { get; set; }

        public virtual Categorium IdcategoriaNavigation { get; set; } = null!;
        public virtual Presentacion IdpresentacionNavigation { get; set; } = null!;
        public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; }
    }
}
