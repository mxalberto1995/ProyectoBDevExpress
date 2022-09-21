using System;
using System.Collections.Generic;

namespace DevExtremePB_Api.Models
{
    public partial class Trabajador
    {
        public Trabajador()
        {
            Ingresos = new HashSet<Ingreso>();
            Venta = new HashSet<Ventum>();
        }

        public int Idtrabajador { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public DateTime FechaNac { get; set; }
        public string NumDocumento { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string Acceso { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Ingreso> Ingresos { get; set; }
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
