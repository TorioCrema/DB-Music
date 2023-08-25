using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Luogo
{
    public uint IdLuogo { get; set; }

    public string Nome { get; set; } = null!;

    public uint Capienza { get; set; }

    public virtual ICollection<Concerto> Concertos { get; set; } = new List<Concerto>();
}
