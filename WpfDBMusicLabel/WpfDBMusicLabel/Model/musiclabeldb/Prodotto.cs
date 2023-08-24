using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Prodotto
{
    public int Codice { get; set; }

    public string Nome { get; set; } = null!;

    public string Descrizione { get; set; } = null!;

    public int Prezzo { get; set; }

    public int QtaProdotta { get; set; }

    public DateTime DataUscita { get; set; }

    public string Tipo { get; set; } = null!;

    public int? Formato { get; set; }

    public int IdAlbum { get; set; }

    public int IdProduttore { get; set; }

    public virtual Produttore IdProduttoreNavigation { get; set; } = null!;

    public virtual Album IdAlbumNavigation { get; set; } = null!;

    public Prodotto(int codice, string nome, string descrizione, int prezzo,
        int qta, DateTime dataUscita, string tipo, int? formato, int idAlbum, int idProduttore)
    {
        Codice = codice;
        Nome = nome;
        Descrizione = descrizione;
        Prezzo = prezzo;
        QtaProdotta = qta;
        DataUscita = dataUscita;
        Tipo = tipo;
        Formato = formato;
        IdAlbum = idAlbum;
        IdProduttore = idProduttore;
    }
}
