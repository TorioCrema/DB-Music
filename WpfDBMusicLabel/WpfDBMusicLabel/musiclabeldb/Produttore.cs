using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Produttore
{
    public uint IdProduttore { get; set; }

    public string Piva { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public uint IndNumCivico { get; set; }

    public string IndVia { get; set; } = null!;

    public string IndCitta { get; set; } = null!;

    public uint NumForniture { get; set; }

    public virtual ICollection<Merchandising> Merchandisings { get; set; } = new List<Merchandising>();

    public virtual ICollection<Prodotto> Prodottos { get; set; } = new List<Prodotto>();
}
