using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Contratto
{
    public decimal Codice { get; set; }

    public decimal Importo { get; set; }

    public DateTime DataInizio { get; set; }

    public DateTime DataFine { get; set; }

    public decimal IdFirmatario { get; set; }

    public virtual Firmatario IdFirmatarioNavigation { get; set; } = null!;
}
