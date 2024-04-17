using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Concerto
{
    public uint IdConcerto { get; set; }

    public DateTime Data { get; set; }

    public uint IdLuogo { get; set; }

    public uint IdTour { get; set; }

    public virtual ICollection<Biglietto> Bigliettos { get; set; } = new List<Biglietto>();

    public virtual Luogo IdLuogoNavigation { get; set; } = null!;

    public virtual Tour IdTourNavigation { get; set; } = null!;

    public virtual ICollection<ProgettoMusicale> IdProgetti { get; set; } = new List<ProgettoMusicale>();

    public uint DispTot { get => calcTot(); }

    private uint calcTot()
    {
        uint tot = 0;
        foreach (var b in Bigliettos)
        {
            tot += b.DispTot;
        }
        return tot;
    }
}
