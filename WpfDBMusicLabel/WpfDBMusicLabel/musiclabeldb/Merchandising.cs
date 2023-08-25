using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Merchandising
{
    public uint Codice { get; set; }

    public string Descrizione { get; set; } = null!;

    public uint Prezzo { get; set; }

    public uint QtaProdotta { get; set; }

    public uint CostoFornituraUnitario { get; set; }

    public uint IdProgetto { get; set; }

    public uint IdProduttore { get; set; }

    public virtual Produttore IdProduttoreNavigation { get; set; } = null!;

    public virtual ProgettoMusicale IdProgettoNavigation { get; set; } = null!;
}
