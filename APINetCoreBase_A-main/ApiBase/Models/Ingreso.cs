using System;
using System.Collections.Generic;

namespace DevExtremePB_Api.Models
{
    public partial class Ingreso
    {
        public Ingreso()
        {
            DetalleIngresos = new HashSet<DetalleIngreso>();
        }

        public int Idingreso { get; set; }
        public int Idtrabajador { get; set; }
        public int Idproveedor { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoComprobante { get; set; } = null!;
        public string Serie { get; set; } = null!;
        public string Correlativo { get; set; } = null!;
        public decimal Igv { get; set; }
        public string Estado { get; set; } = null!;

        public virtual Proveedor IdproveedorNavigation { get; set; } = null!;
        public virtual Trabajador IdtrabajadorNavigation { get; set; } = null!;
        public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; }
    }
}
