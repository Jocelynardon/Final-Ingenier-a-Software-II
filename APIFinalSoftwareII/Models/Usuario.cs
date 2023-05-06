using System;
using System.Collections.Generic;

namespace APIFinalSoftwareII.Models;

public partial class Usuario
{
    public string Usuario1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int TipoUsuario { get; set; }

    public sbyte RealizoVoto { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Genero { get; set; }

    public string? Partido { get; set; }

    public virtual ICollection<Voto> Votos { get; set; } = new List<Voto>();
}
