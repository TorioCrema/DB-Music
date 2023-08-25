using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Album
{
    public uint IdAlbum { get; set; }

    public string Nome { get; set; } = null!;

    public uint IdProgetto { get; set; }

    public uint Durata { get; set; }

    public DateTime DataPubblicazione { get; set; }

    public virtual ProgettoMusicale IdProgettoNavigation { get; set; } = null!;

    public virtual ICollection<Prodotto> Prodottos { get; set; } = new List<Prodotto>();

    public virtual ICollection<Traccia> IdTraccia { get; set; } = new List<Traccia>();
}
