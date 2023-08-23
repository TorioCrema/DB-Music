using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Luogo
{
    public decimal IdLuogo { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Capienza { get; set; }

    public virtual ICollection<Concerto> Concertos { get; set; } = new List<Concerto>();
}
