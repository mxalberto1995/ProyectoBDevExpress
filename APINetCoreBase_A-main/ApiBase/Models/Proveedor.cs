using System;
using System.Collections.Generic;

namespace DevExtremePB_Api.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Ingresos = new HashSet<Ingreso>();
        }

        public int Idproveedor { get; set; }
        public string RazonSocial { get; set; } = null!;
        public string SectorComercial { get; set; } = null!;
        public string TipoDocumento { get; set; } = null!;
        public string NumDocumento { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Url { get; set; }

        public virtual ICollection<Ingreso> Ingresos { get; set; }
    }
}
