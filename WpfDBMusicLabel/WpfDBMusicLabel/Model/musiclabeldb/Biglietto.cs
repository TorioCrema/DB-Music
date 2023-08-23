using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Biglietto
{
    public decimal IdBiglietto { get; set; }

    public decimal Costo { get; set; }

    public string Descrizione { get; set; } = null!;

    public decimal DispTot { get; set; }

    public decimal NumVenduti { get; set; }

    public decimal IdConcerto { get; set; }

    public virtual Concerto IdConcertoNavigation { get; set; } = null!;
}
