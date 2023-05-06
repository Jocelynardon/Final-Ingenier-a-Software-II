using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSoftwareIIModel
{
    public class Usuario
    {
        public string Usuario1 { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int? TipoUsuario { get; set; }

        public sbyte? RealizoVoto { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public string? Genero { get; set; }

        public string? Partido { get; set; }
    }
}
