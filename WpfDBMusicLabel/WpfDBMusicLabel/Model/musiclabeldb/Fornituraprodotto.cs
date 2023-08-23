using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Fornituraprodotto
{
    public decimal Codice { get; set; }

    public decimal Costo { get; set; }

    public decimal IdProduttore { get; set; }

    public virtual Prodotto CodiceNavigation { get; set; } = null!;

    public virtual Produttore IdProduttoreNavigation { get; set; } = null!;
}
