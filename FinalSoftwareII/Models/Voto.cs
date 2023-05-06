using System;
using System.Collections.Generic;

namespace FinalSoftwareII.Models;

public partial class Voto
{
    public int Id { get; set; }

    public string Usuario { get; set; } = null!;

    public string? Partido { get; set; }

    public string? Hora { get; set; }

    public DateTime FechaVoto { get; set; }

    public string? Ip { get; set; }

    public int? VotosValidosTotales { get; set; }

    public int? VotosFraude { get; set; }

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
