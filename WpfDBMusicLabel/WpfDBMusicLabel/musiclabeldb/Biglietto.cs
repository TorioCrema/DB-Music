using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Biglietto
{
    public uint IdBiglietto { get; set; }

    public uint Costo { get; set; }

    public string Descrizione { get; set; } = null!;

    public uint DispTot { get; set; }

    public uint NumVenduti { get; set; }

    public uint IdConcerto { get; set; }

    public virtual Concerto IdConcertoNavigation { get; set; } = null!;
}
