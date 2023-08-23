using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Concerto
{
    public decimal IdConcerto { get; set; }

    public DateTime Data { get; set; }

    public decimal IdLuogo { get; set; }

    public decimal IdTour { get; set; }

    public virtual ICollection<Biglietto> Bigliettos { get; set; } = new List<Biglietto>();

    public virtual Luogo IdLuogoNavigation { get; set; } = null!;

    public virtual Tour IdTourNavigation { get; set; } = null!;

    public virtual ICollection<ProgettoMusicale> IdProgettos { get; set; } = new List<ProgettoMusicale>();
}
