using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Firmatario
{
    public static List<string> Ruoli
    {
        get => new()
            {
                "Producer",
                "Musicista",
                "Vocalist",
                "Nessun Ruolo"
            };
    }

    public uint IdFirmatario { get; set; }

    public string Cf { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public uint IndNumCivico { get; set; }

    public string IndVia { get; set; } = null!;

    public string IndCitta { get; set; } = null!;

    public string? Ruolo { get; set; }

    public virtual ICollection<Contratto> Contratti { get; set; } = new List<Contratto>();

    public virtual ICollection<ProgettoMusicale> IdProgetti { get; set; } = new List<ProgettoMusicale>();

    public virtual ICollection<Traccia> IdTraccia { get; set; } = new List<Traccia>();
}
