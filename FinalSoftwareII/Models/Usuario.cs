using System;
using System.Collections.Generic;

namespace FinalSoftwareII.Models;

public partial class Usuario
{
    public string Usuario1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? TipoUsuario { get; set; }

    public sbyte? RealizoVoto { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Genero { get; set; }

    public string? Partido { get; set; }

    public virtual ICollection<Voto> Votos { get; set; } = new List<Voto>();
}
