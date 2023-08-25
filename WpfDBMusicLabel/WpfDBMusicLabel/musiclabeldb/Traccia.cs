using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Traccia
{
    public uint IdTraccia { get; set; }

    public string Nome { get; set; } = null!;

    public uint IdProgetto { get; set; }

    public DateTime DataPubblicazione { get; set; }

    public uint Durata { get; set; }

    public string? Testo { get; set; }

    public virtual ProgettoMusicale IdProgettoNavigation { get; set; } = null!;

    public virtual ICollection<Album> IdAlbums { get; set; } = new List<Album>();

    public virtual ICollection<Firmatario> IdFirmatarios { get; set; } = new List<Firmatario>();

    public virtual ICollection<ProgettoMusicale> IdProgettos { get; set; } = new List<ProgettoMusicale>();
}
