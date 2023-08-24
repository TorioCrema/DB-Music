using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Album
{
    public int IdAlbum { get; set; }

    public string Nome { get; set; } = null!;

    public int IdProgetto { get; set; }

    public int Durata { get; set; }

    public DateTime DataPubblicazione { get; set; }

    public virtual ProgettoMusicale IdProgettoNavigation { get; set; } = null!;

    public virtual ICollection<Prodotto> Prodottos { get; set; } = new List<Prodotto>();

    public virtual ICollection<Traccia> IdTraccia { get; set; } = new List<Traccia>();

    public Album(int idAlbum, string nome, int idProgetto, int durata, DateTime dataPubblicazione)
    {
        IdAlbum = idAlbum;
        Nome = nome;
        IdProgetto = idProgetto;
        Durata = durata;
        DataPubblicazione = dataPubblicazione;
    }

    override public string ToString()
    {
        return "ID_Album=" + this.IdAlbum.ToString() + " Nome=" + this.Nome.ToString() + " IdProgetto=" + this.IdProgetto.ToString() + " Durata=" + this.Durata.ToString();
    }
}
