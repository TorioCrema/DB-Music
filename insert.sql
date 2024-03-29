insert into Firmatario(CF, Nome, Cognome, Ind_NumCivico, Ind_Via, Ind_Citta, Ruolo) values ('RSSMRA00A01H501A', 'Mario', 'Rossi', 1, 'Via Roma', 'Roma', 'Cantante'),
	('VRDGPP00A01H501A', 'Giuseppe', 'Verdi', 1005, 'Via Sasso Purtroppo', 'Milano', 'Produttore'),
	('ZMASMN01L18F001B', 'Simone', 'Zama', 34, 'Via Riccione', 'Faenza', 'Musicista'),
	('CSHDNS00P08C573D', 'Denis', 'Caushaj', 27, 'Via Emilia Ovest', 'Savignano sul Rubicone', 'Cantante'),
	('MCCPLA00P08C573D', 'Paul', 'McCartney', 1, 'Abbey Road', 'Liverpool', null),
	('LNNJHN00P08C573D', 'John', 'Lennon', 1, 'Abbey Road', 'Liverpool', null),
	('GRGHRN00P08C573D', 'George', 'Harrison', 1, 'Abbey Road', 'Liverpool', null),
	('RNGSTH00P08C573D', 'Ringo', 'Starr', 1, 'Abbey Road', 'Liverpool', null),
	('TWNPTE77FBSJABDI', 'Pete', 'Townshend', 60, 'Kensington Palace Gardens', 'London', null),
	('RGRDLS00P08C573D', 'Roger', 'Daltrey', 60, 'Kensington Palace Gardens', 'London', null);

insert into Progetto_Musicale(Nome, DataFormazione, DataScioglimento, Tipo, NumMembri) values ('The Beatles', '1960-08-01', '1970-04-10', 'Band', 4),
	('The Who', '1964-01-01', null, 'Band', 4),
	('Sined', '2022-10-28', null, 'Solista', null),
	('MarioRossi', '1960-08-01', null, 'Solista', null);

insert into Tour(Nome) values ('The Beatles Tour'),
	('The Who Tour'),
	('Sined Tour'),
	('MarioRossi Tour');

insert into Luogo(Nome, Capienza) values ('Stadio Olimpico', 70000),
	('Stadio San Siro', 76000),
	('Stadio Dall''Ara', 40000),
	('Stadio Renato', 40000),
	('Ippodromo San Siro', 80000);

insert into Contratto(Importo, DataInizio, DataFine, ID_Firmatario) values (100000, '1960-08-01', '1970-04-10', 1),
	(100000, '1964-01-01', '1964-12-31', 2),
	(900000000, '2022-10-28', '2025-12-31', 3),
	(900000000, '2022-10-28', '2025-12-31', 4),
	(100000000, '1960-08-01', '1970-04-10', 5),
	(100000000, '1960-08-01', '1970-04-10', 6),
	(100000000, '1960-08-01', '1970-04-10', 7),
	(100000000, '1960-08-01', '1970-04-10', 8),
	(50000000, '1960-08-01', '1970-04-10', 9),
	(50000000, '1960-08-01', '1970-04-10', 10);

insert into Partecipazione(ID_Firmatario, ID_Progetto) values (1, 4),
	(2, 2),
	(3, 2),
	(4, 3),
	(4, 1),
	(5, 1),
	(6, 1),
	(7, 1),
	(8, 1),
	(9, 2),
	(10, 2);

insert into Concerto(Data, ID_Luogo, ID_Tour) values ('1965-06-01', 1, 1),
	('1965-06-02', 1, 1),
	('1965-06-03', 2, 1),
	('2023-07-31', 3, 2),
	('2023-08-01', 5, 2),
	('2023-08-02', 1, 2),
	('2023-12-12', 1, 3),
	('2023-12-13', 2, 3),
	('2023-12-14', 5, 3),
	('2024-02-28', 4, 4);

insert into Performance(ID_Concerto, ID_Progetto) values (1, 1),
	(1, 2),
	(2, 1),
	(3, 1),
	(4, 2),
	(5, 2),
	(6, 2),
	(7, 3),
	(8, 3),
	(9, 3),
	(10, 4);

insert into Biglietto(Costo, Descrizione, DispTot, NumVenduti, ID_Concerto) values (10000, 'Prato', 10000, 500, 1),
	(20000, 'Tribuna', 60000, 25000, 1),
	(10000, 'Prato', 10000, 150, 2),
	(20000, 'Tribuna', 60000, 500, 2),
	(10000, 'Prato', 6000, 5000, 3),
	(20000, 'Tribuna', 70000, 50000, 3),
	(9000, 'Prato', 10000, 500, 4),
	(15000, 'Tribuna', 30000, 25000, 4),
	(9000, 'Prato', 80000, 20000, 5),
	(9000, 'Prato', 6000, 5000, 6),
	(20000, 'Tribuna', 64000, 50000, 6),
	(12000, 'Prato', 10000, 10000, 7),
	(25000, 'Tribuna', 60000, 25000, 7),
	(11000, 'Prato', 16000, 9000, 8),
	(22000, 'Tribuna', 60000, 25000, 8),
	(13000, 'Prato', 80000, 20000, 9),
	(5000, 'Prato', 40000, 0, 10);

insert into Traccia(Nome, ID_Progetto, DataPubblicazione, Durata, Testo) values ('Obladi Oblada', 1, '1968-06-01', 188, 'La la la la la la'),
	('Hey Jude', 1, '1968-12-05', 425, 'Hey Jude, dont make it bad'),
	('Behind Blue Eyes', 2, '1965-06-01', 221, 'No one knows what its like'),
	('My Generation', 2, '1960-06-01', 198, 'People try to put us d-down'),
	('Nomadi', 3, '2022-10-28', 155, 'Stiamo in giro come nomadi'),
	("Filo d\'aria", 3, '2023-12-12', 200, 'Ora chiudo le finestre');

insert into Collaborazione(ID_Firmatario, ID_Traccia) values (3, 1),
	(3, 5);

insert into Feature(ID_Progetto, ID_Traccia) values (1, 6);

insert into Album(Nome, ID_Progetto, Durata, DataPubblicazione) values ('White Album', 1, 613, '1968-12-05'),
	("Who's Next", 2, 419, '1971-08-14'),
	("Questa notte deve ancora parlare", 3, 355, '2023-12-12');

insert into Composizione(ID_Album, ID_Traccia) values (1, 1),
	(1, 2),
	(2, 3),
	(2, 4),
	(3, 5),
	(3, 6);

insert into Produttore(PIVA, Nome, Ind_NumCivico, Ind_Via, Ind_Citta, NumForniture) values ('12345678901', 'Sony', 1, 'Via Sony', 'Tokyo', 6),
	('12345678902', 'EMI', 2, 'Via EMI', 'London', 4);

insert into Prodotto(Nome, Descrizione, Prezzo, QtaProdotta, CostoFornitura, DataUscita, Tipo, Formato, ID_Album, ID_Produttore) values ('White Album', 'Album dei Beatles', 3000, 200000, 500000000, '1968-12-05', 'Vinile', 33, 1, 1),
	('White Album', 'Album dei Beatles', 1700, 100000, 50000000, '1968-12-05', 'CD', null, 1, 1),
	("Who's Next", 'Album dei The Who', 3200, 50000, 10000000, '1971-08-14', 'Vinile', 33, 2, 1),
	("Who's Next", 'Album dei The Who', 1500, 50000, 10000000, '1971-08-14', 'CD', null, 2, 1),
	('Questa notte deve ancora parlare', 'Album di Sined', 3500, 999999, 1000000000, '2023-12-12', 'Vinile', 33, 3, 2),
	('Questa notte deve ancora parlare', 'Album di Sined', 2000, 999999, 1000000000, '2023-12-12', 'CD', null, 3, 2);

insert into Merchandising(Descrizione, Prezzo, QtaProdotta, CostoFornituraUnitario, ID_Progetto, ID_Produttore) values ('Maglietta Beatles', 2000, 4000, 500, 1, 1),
	('Maglietta The Who', 2000, 10000, 1000, 2, 1),
	('Maglietta Sined', 5000, 15000, 2000, 4, 2),
	('Felpa Simone Zama', 4500, 7000, 2000, 3, 2);