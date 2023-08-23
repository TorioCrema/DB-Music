using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Fornituramerch
{
    public decimal Codice { get; set; }

    public decimal QtaProdotta { get; set; }

    public decimal PrezzoFornituraUnitario { get; set; }

    public decimal IdProduttore { get; set; }

    public virtual Merchandising CodiceNavigation { get; set; } = null!;

    public virtual Produttore IdProduttoreNavigation { get; set; } = null!;
}
