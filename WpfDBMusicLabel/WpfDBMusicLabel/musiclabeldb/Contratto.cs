using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Contratto
{
    public uint Codice { get; set; }

    public uint Importo { get; set; }

    public DateTime DataInizio { get; set; }

    public DateTime DataFine { get; set; }

    public uint IdFirmatario { get; set; }

    public virtual Firmatario IdFirmatarioNavigation { get; set; } = null!;
}
