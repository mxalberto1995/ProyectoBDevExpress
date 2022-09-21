using System;
using System.Collections.Generic;

namespace DevExtremePB_Api.Models
{
    public partial class DetalleIngreso
    {
        public DetalleIngreso()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int IddetalleIngreso { get; set; }
        public int Idingreso { get; set; }
        public int Idarticulo { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockInicial { get; set; }
        public int StockActual { get; set; }
        public DateTime FechaProduccion { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public virtual Articulo IdarticuloNavigation { get; set; } = null!;
        public virtual Ingreso IdingresoNavigation { get; set; } = null!;
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
