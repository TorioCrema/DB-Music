using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Merchandising
{
    public decimal Codice { get; set; }

    public string Descrizione { get; set; } = null!;

    public decimal Prezzo { get; set; }

    public decimal IdProgetto { get; set; }

    public virtual Fornituramerch? Fornituramerch { get; set; }

    public virtual ProgettoMusicale IdProgettoNavigation { get; set; } = null!;
}
