using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class ProgettoMusicale
{
    public uint IdProgetto { get; set; }

    public string Nome { get; set; } = null!;

    public DateTime DataFormazione { get; set; }

    public DateTime? DataScioglimento { get; set; }

    public string Tipo { get; set; } = null!;

    public byte? NumMembri { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<Merchandising> Merchandisings { get; set; } = new List<Merchandising>();

    public virtual ICollection<Traccium> Traccia { get; set; } = new List<Traccium>();

    public virtual ICollection<Concerto> IdConcertos { get; set; } = new List<Concerto>();

    public virtual ICollection<Firmatario> IdFirmatarios { get; set; } = new List<Firmatario>();

    public virtual ICollection<Traccium> IdTraccia { get; set; } = new List<Traccium>();
}
