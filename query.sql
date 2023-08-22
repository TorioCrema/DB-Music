-- Visualizzazione della discografia di un progetto musicale

select A.Nome, A.Durata, A.DataPubblicazione, P.Nome as Autore
    from Album A join Progetto_Musicale P on (A.ID_Progetto = P.ID_Progetto)
    where A.ID_Progetto = 1;

-- Visualizzazione delle apparizioni come feature di un progetto musicale
select T.Nome, T.DataPubblicazione, T.Durata
    from Feature F join Traccia T on (F.ID_Traccia = T.ID_Traccia)
    where F.ID_Progetto = 1;

-- Visualizzazione di tutto il merchandising di un progetto musicale
select Codice, Descrizione, Prezzo/100 as Costo, QtaProdotta, CostFornituraUnitario/100 as CostoFornituraUnitario, ID_Produttore from Merchandising where ID_Progetto = 1;

-- Visualizzazione dei biglietti disponibili per un concerto
select Descrizione, Costo/100 as Prezzo, DispTot-NumVenduti as NumeroDisponibili from Biglietto B where NumVenduti < DispTot and ID_Concerto = 1;

-- Visualizzazione delle edizioni fisiche di un album
select PA.Nome, PA.Descrizione, PA.Prezzo, PA.QtaProdotta, PA.CostoFornitura,
    PA.DataUscita, PA.Tipo, PA.Formato, PA.Album, PM.Nome as Autore
    from (select P.Nome, P.Descrizione, P.Prezzo/100 as Prezzo, P.QtaProdotta,
        P.CostoFornitura/100 as CostoFornitura, P.DataUscita, P.Tipo, P.Formato,
        A.Nome as Album from Prodotto P join Album A on (P.ID_Album = A.ID_Album)
        where P.ID_Album = 1) PA 
    join Progetto_Musicale PM on (PA.ID_Progetto = PM.ID_Progetto);

-- Visualizzazione delle informazioni dei concerti appartenenti ad un tour, inclusa la quantita' totale di biglietti venduti e le performance
select CB.Data, CB.NumeroVenduti, L.Nome as Venue from (select C.Data, ND.NumeroVenduti, C.ID_Luogo, C.ID_Concerto from Concerto C join 
    (select sum(NumVenduti) as NumeroVenduti, ID_Concerto from Biglietto B group by B.ID_Concerto) ND
    on (C.ID_Concerto = ND.ID_Concerto) where C.ID_Tour = 1) CB join Luogo L on (CB.ID_Luogo = L.ID_Luogo);

-- Visualizzazione del totale dei biglietti venduti da un progetto musicale
select sum(Venduti) NumBigliettiVendutiTot
	from (select sum(B.NumVenduti) as Venduti, C.ID_Concerto from Biglietto B join Concerto C on (B.ID_Concerto = C.ID_Concerto) group by C.ID_Concerto) TotVenduti
	join Performance P on (TotVenduti.ID_Concerto = P.ID_Concerto) where P.ID_Progetto = 1;