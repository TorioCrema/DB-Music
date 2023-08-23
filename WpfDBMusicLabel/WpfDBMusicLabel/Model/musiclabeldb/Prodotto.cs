using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Prodotto
{
    public decimal Codice { get; set; }

    public string Nome { get; set; } = null!;

    public string Descrizione { get; set; } = null!;

    public decimal Prezzo { get; set; }

    public decimal QtaProdotta { get; set; }

    public DateTime DataUscita { get; set; }

    public string Tipo { get; set; } = null!;

    public decimal? Formato { get; set; }

    public decimal IdAlbum { get; set; }

    public virtual Fornituraprodotto? Fornituraprodotto { get; set; }

    public virtual Album IdAlbumNavigation { get; set; } = null!;
}
