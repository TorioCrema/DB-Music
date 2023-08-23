using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Produttore
{
    public decimal IdProduttore { get; set; }

    public string Piva { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public decimal IndNumCivico { get; set; }

    public string IndVia { get; set; } = null!;

    public string IndCitta { get; set; } = null!;

    public string NumForniture { get; set; } = null!;

    public virtual ICollection<Fornituramerch> Fornituramerches { get; set; } = new List<Fornituramerch>();

    public virtual ICollection<Fornituraprodotto> Fornituraprodottos { get; set; } = new List<Fornituraprodotto>();
}
