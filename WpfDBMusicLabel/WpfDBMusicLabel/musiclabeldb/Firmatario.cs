using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Firmatario
{
    public uint IdFirmatario { get; set; }

    public string Cf { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public uint IndNumCivico { get; set; }

    public string IndVia { get; set; } = null!;

    public string IndCitta { get; set; } = null!;

    public string? Ruolo { get; set; }

    public virtual ICollection<Contratto> Contrattos { get; set; } = new List<Contratto>();

    public virtual ICollection<ProgettoMusicale> IdProgettos { get; set; } = new List<ProgettoMusicale>();

    public virtual ICollection<Traccium> IdTraccia { get; set; } = new List<Traccium>();
}
