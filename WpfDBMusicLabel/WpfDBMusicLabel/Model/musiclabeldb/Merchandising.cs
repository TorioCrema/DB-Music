using System;
using System.Collections.Generic;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class Merchandising
{
    public int Codice { get; set; }

    public string Descrizione { get; set; } = null!;

    public int Prezzo { get; set; }

    public int QtaProdotta { get; set; }

    public int CostoFornituraUnitario { get; set; }

    public int IdProgetto { get; set; }

    public int IdProduttore { get; set; }

    public virtual Produttore IdProduttoreNavigation { get; set; } = null!;

    public virtual ProgettoMusicale IdProgettoNavigation { get; set; } = null!;

    public Merchandising(int codice, string desc, int prezzo, int qtaProd, int costoUnitario, int idProgetto, int idProduttore)
    {
        Codice = codice;
        Descrizione = desc;
        Prezzo = prezzo;
        QtaProdotta = qtaProd;
        CostoFornituraUnitario = costoUnitario;
        IdProgetto = idProgetto;
        IdProduttore = idProduttore;
    }
}
