using System;
using System.Collections.Generic;

namespace DevExtremePB_Api.Models
{
    public partial class DetalleVentum
    {
        public int IddetalleVenta { get; set; }
        public int Idventa { get; set; }
        public int IddetalleIngreso { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Descuento { get; set; }

        public virtual DetalleIngreso IddetalleIngresoNavigation { get; set; } = null!;
        public virtual Ventum IdventaNavigation { get; set; } = null!;
    }
}
