using System;
using System.Collections.Generic;

namespace DevExtremePB_Api.Models
{
    public partial class Ventum
    {
        public Ventum()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int Idventa { get; set; }
        public int Idcliente { get; set; }
        public int Idtrabajador { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoComprobante { get; set; } = null!;
        public string Serie { get; set; } = null!;
        public string Correlativo { get; set; } = null!;
        public decimal Igv { get; set; }

        public virtual Cliente IdclienteNavigation { get; set; } = null!;
        public virtual Trabajador IdtrabajadorNavigation { get; set; } = null!;
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
