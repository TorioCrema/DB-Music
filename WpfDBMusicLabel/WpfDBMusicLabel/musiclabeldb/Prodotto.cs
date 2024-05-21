using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Prodotto
{
    public uint Codice { get; set; }

    public string Nome { get; set; } = null!;

    public string Descrizione { get; set; } = null!;

    public uint Prezzo { get; set; }

    public string PrezzoStr { get => (Prezzo / 100).ToString("0.00") + "€"; }

    public uint QtaProdotta { get; set; }

    public uint CostoFornitura { get; set; }

    public DateTime DataUscita { get; set; }

    public string Tipo { get; set; } = null!;

    public byte? Formato { get; set; }

    public uint IdAlbum { get; set; }

    public uint IdProduttore { get; set; }

    public virtual Album IdAlbumNavigation { get; set; } = null!;

    public virtual Produttore IdProduttoreNavigation { get; set; } = null!;
}
