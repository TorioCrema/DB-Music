using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Traccia
{
    public uint IdTraccia { get; set; }

    public string Nome { get; set; } = null!;

    public uint IdProgetto { get; set; }

    public DateTime DataPubblicazione { get; set; }

    public uint Durata { get; set; }

    public string DurataStr { get => (Durata / 60).ToString() + ":" + ((Durata % 60).ToString().Length < 2 ? "0" + (Durata % 60).ToString() : (Durata % 60).ToString()); }

    public string? Testo { get; set; }

    public virtual ProgettoMusicale IdProgettoNavigation { get; set; } = null!;

    public virtual ICollection<Album> IdAlbums { get; set; } = new List<Album>();

    public virtual ICollection<Firmatario> IdFirmatarios { get; set; } = new List<Firmatario>();

    public virtual ICollection<ProgettoMusicale> IdProgettos { get; set; } = new List<ProgettoMusicale>();
}
