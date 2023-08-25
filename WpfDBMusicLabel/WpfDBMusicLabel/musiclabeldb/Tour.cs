using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Tour
{
    public uint IdTour { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Concerto> Concertos { get; set; } = new List<Concerto>();
}
