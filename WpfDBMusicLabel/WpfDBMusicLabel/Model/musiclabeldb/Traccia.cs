using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Traccia
{
    public int IdTraccia { get; set; }

    public string Nome { get; set; } = null!;

    public int IdProgetto { get; set; }

    public DateTime DataPubblicazione { get; set; }

    public int Durata { get; set; }

    public string? Testo { get; set; }

    public virtual ProgettoMusicale IdProgettoNavigation { get; set; } = null!;

    public virtual ICollection<Album> IdAlbums { get; set; } = new List<Album>();

    public virtual ICollection<Firmatario> IdFirmatarios { get; set; } = new List<Firmatario>();

    public virtual ICollection<ProgettoMusicale> IdProgettos { get; set; } = new List<ProgettoMusicale>();

    public Traccia(int idTraccia, string nome, int idProgetto, DateTime data, int durata, string? testo)
    {
        IdTraccia = idTraccia;
        Nome = nome;
        IdProgetto = idProgetto;
        DataPubblicazione = data;
        Durata = durata;
        Testo = testo;
    }
}
