insert into Firmatario values (1, 'RSSMRA00A01H501A', 'Mario', 'Rossi', 1, 'Via Roma', 'Roma', 'Cantante');
insert into Firmatario values (2, 'VRDGPP00A01H501A', 'Giuseppe', 'Verdi', 1005, 'Via Sasso Purtroppo', 'Milano', 'Produttore');
insert into Firmatario values (3, 'ZMASMN01L18F001B', 'Simone', 'Zama', 34, 'Via Riccione', 'Faenza', 'Musicista');
insert into Firmatario values (4, 'CSHDNS00P08C573D', 'Denis', 'Caushaj', 27, 'Via Emilia Ovest', 'Savignano sul Rubicone', 'Cantante');
insert into Firmatario values (5, 'MCCPLA00P08C573D', 'Paul', 'McCartney', 1, 'Abbey Road', 'Liverpool', null);
insert into Firmatario values (6, 'LNNJHN00P08C573D', 'John', 'Lennon', 1, 'Abbey Road', 'Liverpool', null);
insert into Firmatario values (7, 'GRGHRN00P08C573D', 'George', 'Harrison', 1, 'Abbey Road', 'Liverpool', null);
insert into Firmatario values (8, 'RNGSTH00P08C573D', 'Ringo', 'Starr', 1, 'Abbey Road', 'Liverpool', null);
insert into Firmatario values (9, 'TWNPTE77FBSJABDI', 'Pete', 'Townshend', 60, 'Kensington Palace Gardens', 'London', null);
insert into Firmatario values (10, 'RGRDLS00P08C573D', 'Roger', 'Daltrey', 60, 'Kensington Palace Gardens', 'London', null);

insert into Progetto_musicale values (1, 'The Beatles', '1960-08-01', '1970-04-10', 'Band', 4);
insert into Progetto_musicale values (2, 'The Who', '1964-01-01', null, 'Band', 4);
insert into Progetto_musicale values (3, 'Sined', '2022-10-28', null, 'Solista', null);
insert into Progetto_musicale values (4, 'MarioRossi', '1960-08-01', null, 'Solista', null);

insert into Tour values (1, 'The Beatles Tour');
insert into Tour values (2, 'The Who Tour');
insert into Tour values (3, 'Sined Tour');
insert into Tour values (4, 'MarioRossi Tour');

insert into Luogo values (1, 'Stadio Olimpico', 70000);
insert into Luogo values (2, 'Stadio San Siro', 76000);
insert into Luogo values (3, 'Stadio Dall''Ara', 40000);
insert into Luogo values (4, 'Stadio Renato', 40000);
insert into Luogo values (5, 'Ippodromo San Siro', 80000);

insert into Contratto values (1, 1000, '1960-08-01', '1970-04-10', 1);
insert into Contratto values (2, 1000, '1964-01-01', '1964-12-31', 2);
insert into Contratto values (3, 9000000, '2022-10-28', '2025-12-31', 3);
insert into Contratto values (4, 9000000, '2022-10-28', '2025-12-31', 4);
insert into Contratto values (5, 1000000, '1960-08-01', '1970-04-10', 5);
insert into Contratto values (6, 1000000, '1960-08-01', '1970-04-10', 6);
insert into Contratto values (7, 1000000, '1960-08-01', '1970-04-10', 7);
insert into Contratto values (8, 1000000, '1960-08-01', '1970-04-10', 8);
insert into Contratto values (9, 500000, '1960-08-01', '1970-04-10', 9);
insert into Contratto values (10, 500000, '1960-08-01', '1970-04-10', 10);

insert into Partecipazione values (1, 4);
insert into Partecipazione values (2, 2);
insert into Partecipazione values (3, 2);
insert into Partecipazione values (4, 3);
insert into Partecipazione values (4, 1);
insert into Partecipazione values (5, 1);
insert into Partecipazione values (6, 1);
insert into Partecipazione values (7, 1);
insert into Partecipazione values (8, 1);
insert into Partecipazione values (9, 2);
insert into Partecipazione values (10, 2);

insert into Concerto values (1, '1965-06-01', 1, 1);
insert into Concerto values (2, '1965-06-02', 1, 1);
insert into Concerto values (3, '1965-06-03', 2, 1);
insert into Concerto values (4, '2023-07-31', 3, 2);
insert into Concerto values (5, '2023-08-01', 5, 2);
insert into Concerto values (6, '2023-08-02', 1, 2);
insert into Concerto values (7, '2023-12-12', 1, 3);
insert into Concerto values (8, '2023-12-13', 2, 3);
insert into Concerto values (9, '2023-12-14', 5, 3);
insert into Concerto values (10, '2024-02-28', 4, 4);

insert into Performance values (1, 1);
insert into Performance values (2, 1);
insert into Performance values (3, 1);
insert into Performance values (4, 2);
insert into Performance values (5, 2);
insert into Performance values (6, 2);
insert into Performance values (7, 3);
insert into Performance values (8, 3);
insert into Performance values (9, 3);
insert into Performance values (10, 4);

insert into Biglietto values (1, 100, 'Prato', 10000, 500, 1);
insert into Biglietto values (2, 200, 'Tribuna', 60000, 25000, 1);
insert into Biglietto values (3, 100, 'Prato', 10000, 150, 2);
insert into Biglietto values (4, 200, 'Tribuna', 60000, 500, 2);
insert into Biglietto values (5, 100, 'Prato', 6000, 5000, 3);
insert into Biglietto values (6, 200, 'Tribuna', 70000, 50000, 3);
insert into Biglietto values (7, 90, 'Prato', 10000, 500, 4);
insert into Biglietto values (8, 150, 'Tribuna', 30000, 25000, 4);
insert into Biglietto values (9, 90, 'Prato', 80000, 20000, 5);
insert into Biglietto values (10, 90, 'Prato', 6000, 5000, 6);
insert into Biglietto values (11, 200, 'Tribuna', 64000, 50000, 6);
insert into Biglietto values (12, 120, 'Prato', 10000, 10000, 7);
insert into Biglietto values (13, 250, 'Tribuna', 60000, 25000, 7);
insert into Biglietto values (14, 110, 'Prato', 16000, 9000, 8);
insert into Biglietto values (15, 220, 'Tribuna', 60000, 25000, 8);
insert into Biglietto values (16, 130, 'Prato', 80000, 20000, 9);
insert into Biglietto values (17, 50, 'Prato', 40000, 0, 10);

insert into Traccia values (1, 'Obladi Oblada', 1, '1968-06-01', 188, 'La la la la la la');
insert into Traccia values (2, 'Hey Jude', 1, '1968-12-05', 425, 'Hey Jude, dont make it bad');
insert into Traccia values (3, 'Behind Blue Eyes', 2, '1965-06-01', 221, 'No one knows what its like');
insert into Traccia values (4, 'My Generation', 2, '1960-06-01', 198, 'People try to put us d-down');
insert into Traccia values (5, 'Nomadi', 3, '2022-10-28', 155, 'Stiamo in giro come nomadi');
insert into Traccia values (6, 'Filo d\'aria', 3, '2023-12-12', 200, 'Ora chiudo le finestre');

insert into Collaborazione values (3, 1);
insert into Collaborazione values (3, 5);

insert into Feature values (1, 6);

insert into Album values (1, 'White Album', 1, 613, '1968-12-05');
insert into Album values (2, 'Who\'s Next', 2, 419, '1971-08-14');
insert into Album values (3, 'Nomadi', 3, 355, '2023-12-12');

insert into Composizione values (1, 1);
insert into Composizione values (1, 2);
insert into Composizione values (2, 3);
insert into Composizione values (2, 4);
insert into Composizione values (3, 5);
insert into Composizione values (3, 6);

insert into Prodotto values (1, 'White Album', 'Album dei Beatles', 30, 200000, '1968-12-05', 'Vinile', 33, 1);
insert into Prodotto values (2, 'White Album', 'Album dei Beatles', 17, 100000, '1968-12-05', 'CD', null, 1);
insert into Prodotto values (3, 'Who\'s Next', 'Album dei The Who', 32, 50000, '1971-08-14', 'Vinile', 33, 2);
insert into Prodotto values (4, 'Who\'s Next', 'Album dei The Who', 15, 50000, '1971-08-14', 'CD', null, 2);
insert into Prodotto values (5, 'Nomadi', 'Album di Sined', 35, 999999, '2023-12-12', 'Vinile', 33, 3);
insert into Prodotto values (6, 'Nomadi', 'Album di Sined', 20, 999999, '2023-12-12', 'CD', null, 3);

insert into Produttore values (1, '12345678901', 'Sony', 1, 'Via Sony', 'Tokyo', 6);
insert into Produttore values (2, '12345678902', 'EMI', 2, 'Via EMI', 'London', 4);

insert into FornituraProdotto values (1, 1000000, 1);
insert into FornituraProdotto values (2, 500000, 1);
insert into FornituraProdotto values (3, 250000, 1);
insert into FornituraProdotto values (4, 250000, 1);
insert into FornituraProdotto values (5, 999999, 2);
insert into FornituraProdotto values (6, 999999, 2);

insert into Merchandising values (1, 'Maglietta Beatles', 20, 1);
insert into Merchandising values (2, 'Maglietta The Who', 20, 2);
insert into Merchandising values (3, 'Maglietta Sined', 50, 4);
insert into Merchandising values (4, 'Felpa Simone Zama', 45, 3);

insert into FornituraMerch values (1, 1000000, 5, 1);
insert into FornituraMerch values (2, 500000, 5, 1);
insert into FornituraMerch values (3, 1000000, 15, 2);
insert into FornituraMerch values (4, 1000000, 15, 2);