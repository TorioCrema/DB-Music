-- Visualizzazione della discografia di un progetto musicale
select A.Nome, A.Durata, A.DataPubblicazione, P.Nome as Autore
    from Album A 
    join Progetto_Musicale P on A.ID_Progetto = P.ID_Progetto
    where A.ID_Progetto = ?;

-- Visualizzazione delle apparizioni come feature di un progetto musicale
select T.Nome, T.DataPubblicazione, T.Durata
    from Feature F 
    join Traccia T on F.ID_Traccia = T.ID_Traccia
    where F.ID_Progetto = ?;

-- Visualizzazione di tutto il merchandising di un progetto musicale
select Codice, Descrizione, Prezzo/100 as Costo, QtaProdotta, CostoFornituraUnitario/100 as CostoFornituraUnitario, ID_Produttore
    from Merchandising
    where ID_Progetto = ?;

-- Visualizzazione dei biglietti disponibili per un concerto
select Descrizione, Costo/100 as Prezzo, DispTot-NumVenduti as NumeroDisponibili
    from Biglietto B
    where NumVenduti < DispTot and ID_Concerto = ?;

-- Visualizzazione delle edizioni fisiche di un album
select P.Codice, P.Nome, P.Descrizione, P.DataUscita, P.Prezzo/100 as Prezzo, P.QtaProdotta, P.CostoFornitura/100 as CostoFornitura, P.Tipo, P.Formato, A.Nome as NomeAlbum
    from Prodotto P
    join Album A on P.ID_Album = A.ID_Album
    where P.ID_Album = ?

-- Visualizzazione delle informazioni dei concerti appartenenti ad un tour, inclusa la quantita' totale di biglietti venduti e le performance
select C.ID_Concerto, C.Data, L.Nome, coalesce(sum(B.NumVenduti), 0) as BigliettiVenduti
    from Concerto C
    join Luogo L on C.ID_Luogo = L.ID_Luogo
    left join Biglietto B on B.ID_Concerto = C.ID_Concerto
    where ID_Tour = ?
    group by C.ID_Concerto, C.Data, L.Nome;

-- Visualizzazione del totale dei biglietti venduti da un progetto musicale
select coalesce(sum(B.NumVenduti), 0) as Venduti
    from Progetto_Musicale Proj
    join Performance Perf on Proj.ID_Progetto = Perf.ID_Progetto
    join Concerto C on C.ID_Concerto = Perf.ID_Concerto
    left join Biglietto B on B.ID_Concerto = C.ID_Concerto
    where Proj.ID_Progetto = ?

-- Visualizzazione di tutti i concerti soldout di un tour
SELECT ID_Concerto, Data, ID_Luogo, Nome
    FROM (SELECT C.ID_Concerto, C.Data, C.ID_Luogo, L.Nome, SUM(B.DispTot) as dispSum, SUM(B.NumVenduti) as vendSum
          FROM Tour T
          JOIN Concerto C on T.ID_Tour = C.ID_Tour
          JOIN Biglietto B on B.ID_Concerto = C.ID_Concerto
          JOIN Luogo L on L.ID_Luogo = C.ID_Luogo
          WHERE T.ID_Tour = ? GROUP BY C.ID_Concerto) O
    WHERE O.dispSum = O.vendSum;
    
START transaction;
-- Cancellazione di un concerto --
delete C
	from Concerto C 
    where ID_Concerto = 1;
    
-- Cancellazione di un album --
delete A
	from Album A 
    where ID_Album = 1;
    
-- Cancellazione di una traccia --
delete T
	from Traccia T
    where ID_Traccia = 1;

rollback;