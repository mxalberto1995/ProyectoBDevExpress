using System;
using System.Collections.Generic;

namespace DevExtremePB_Api.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Ventum>();
        }

        public int Idcliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Apellidos { get; set; }
        public string? Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string TipoDocumento { get; set; } = null!;
        public string NumDocumento { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
