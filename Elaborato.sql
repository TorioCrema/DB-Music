-- *********************************************
-- * Standard SQL generation                   
-- *--------------------------------------------
-- * DB-MAIN version: 11.0.2              
-- * Generator date: Sep 20 2021              
-- * Generation date: Mon Jul 31 11:31:07 2023 
-- * LUN file: /home/torio/git/DB-Music/Elaborato.lun 
-- * Schema: MUSICLABELDB/SQL 
-- ********************************************* 


-- Database Section
-- ________________ 

create database MUSICLABELDB;

use MUSICLABELDB;

-- DBSpace Section
-- _______________


-- Tables Section
-- _____________ 

create table Album (
     ID_Album INT UNSIGNED not null AUTO_INCREMENT,
     Nome varchar(50) not null,
     ID_Progetto INT UNSIGNED not null,
     Durata INT UNSIGNED not null,
     DataPubblicazione date not null,
     constraint ID_ALBUM_ID primary key (ID_Album),
     constraint SID_ALBUM_ID unique (ID_Progetto, Nome));

create table Biglietto (
     ID_Biglietto INT UNSIGNED not null AUTO_INCREMENT,
     Costo INT UNSIGNED not null,
     Descrizione varchar(50) not null,
     DispTot INT UNSIGNED not null,
     NumVenduti INT UNSIGNED not null,
     ID_Concerto INT UNSIGNED not null,
     constraint ID_BIGLIETTO_ID primary key (ID_Biglietto))
     constraint Limite_Vendita check(NumVenduti <= DispTot);

create table Collaborazione (
     ID_Firmatario INT UNSIGNED not null,
     ID_Traccia INT UNSIGNED not null,
     constraint ID_Collaborazione_ID primary key (ID_Traccia, ID_Firmatario));

create table Composizione (
     ID_Album INT UNSIGNED not null,
     ID_Traccia INT UNSIGNED not null,
     constraint ID_Composizione_ID primary key (ID_Album, ID_Traccia));

create table Concerto (
     ID_Concerto INT UNSIGNED not null AUTO_INCREMENT,
     Data date not null,
     ID_Luogo INT UNSIGNED not null,
     ID_Tour INT UNSIGNED not null,
     constraint ID_CONCERTO_ID primary key (ID_Concerto));

create table Contratto (
     Codice INT UNSIGNED not null AUTO_INCREMENT,
     Importo INT UNSIGNED not null,
     DataInizio date not null,
     DataFine date not null,
     ID_Firmatario INT UNSIGNED not null,
     constraint ID_CONTRATTO_ID primary key (Codice));

create table Feature (
     ID_Progetto INT UNSIGNED not null,
     ID_Traccia INT UNSIGNED not null,
     constraint ID_Feature_ID primary key (ID_Traccia, ID_Progetto));

create table Firmatario (
     ID_Firmatario INT UNSIGNED not null AUTO_INCREMENT,
     CF char(16) not null,
     Nome varchar(50) not null,
     Cognome varchar(50) not null,
     Ind_NumCivico INT UNSIGNED not null,
     Ind_Via varchar(50) not null,
     Ind_Citta varchar(50) not null,
     Ruolo varchar(50),
     constraint ID_FIRMATARIO_ID primary key (ID_Firmatario),
     constraint SID_FIRMATARIO_ID unique (CF));

create table Luogo (
     ID_Luogo INT UNSIGNED not null AUTO_INCREMENT,
     Nome varchar(50) not null,
     Capienza INT UNSIGNED not null,
     constraint ID_LUOGO_ID primary key (ID_Luogo));

create table Merchandising (
     Codice INT UNSIGNED not null AUTO_INCREMENT,
     Descrizione varchar(50) not null,
     Prezzo INT UNSIGNED not null,
     QtaProdotta INT UNSIGNED not null,
     CostoFornituraUnitario INT UNSIGNED not null,
     ID_Progetto INT UNSIGNED not null,
     ID_Produttore INT UNSIGNED not null,
     constraint ID_MERCHANDISING_ID primary key (Codice));

create table Partecipazione (
     ID_Firmatario INT UNSIGNED not null,
     ID_Progetto INT UNSIGNED not null,
     constraint ID_Partecipazione_ID primary key (ID_Progetto, ID_Firmatario));

create table Performance (
     ID_Concerto INT UNSIGNED not null,
     ID_Progetto INT UNSIGNED not null,
     constraint ID_Performance_ID primary key (ID_Concerto, ID_Progetto));

create table Prodotto (
     Codice INT UNSIGNED not null AUTO_INCREMENT,
     Nome varchar(50) not null,
     Descrizione varchar(50) not null,
     Prezzo INT UNSIGNED not null,
     QtaProdotta INT UNSIGNED not null,
     CostoFornitura INT UNSIGNED not null,
     DataUscita date not null,
     Tipo varchar(10) not null,
     Formato TINYINT UNSIGNED,
     ID_Album INT UNSIGNED not null,
     ID_Produttore INT UNSIGNED not null,
     constraint ID_PRODOTTO_ID primary key (Codice));

create table Produttore (
     ID_Produttore INT UNSIGNED not null AUTO_INCREMENT,
     PIVA char(11) not null,
     Nome varchar(50) not null,
     Ind_NumCivico INT UNSIGNED not null,
     Ind_Via varchar(50) not null,
     Ind_Citta varchar(50) not null,
     NumForniture INT UNSIGNED not null,
     constraint ID_PRODUTTORE_ID primary key (ID_Produttore),
     constraint SID_PRODUTTORE_ID unique (PIVA));

create table Progetto_Musicale (
     ID_Progetto INT UNSIGNED not null AUTO_INCREMENT,
     Nome varchar(50) not null,
     DataFormazione date not null,
     DataScioglimento date,
     Tipo varchar(50) not null,
     NumMembri TINYINT UNSIGNED,
     constraint ID_PROGETTO_MUSICALE_ID primary key (ID_Progetto));

create table Tour (
     ID_Tour INT UNSIGNED not null AUTO_INCREMENT,
     Nome varchar(50) not null,
     constraint ID_TOUR_ID primary key (ID_Tour));

create table Traccia (
     ID_Traccia INT UNSIGNED not null AUTO_INCREMENT,
     Nome varchar(50) not null,
     ID_Progetto INT UNSIGNED not null,
     DataPubblicazione date not null,
     Durata INT UNSIGNED not null,
     Testo varchar(10000),
     constraint ID_TRACCIA_ID primary key (ID_Traccia),
     constraint SID_TRACCIA_ID unique (ID_Progetto, Nome));


-- Constraints Section
-- ___________________ 

-- alter table ALBUM add constraint FKScrittura_Album
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table Album add foreign key (ID_Progetto) references Progetto_Musicale(ID_Progetto) on delete cascade;

-- alter table BIGLIETTO add constraint FKDisposizione_FK
--      foreign key (ID_Concerto)
--      references CONCERTO;

alter table Biglietto add foreign key (ID_Concerto) references Concerto(ID_Concerto) on delete cascade;

-- alter table Collaborazione add constraint FKCol_TRA
--      foreign key (ID_Traccia)
--      references TRACCIA;

alter table Collaborazione add foreign key (ID_Traccia) references Traccia(ID_TRACCIA) on delete cascade;

-- alter table Collaborazione add constraint FKCol_FIR_FK
--      foreign key (ID_Firmatario)
--      references FIRMATARIO;

alter table Collaborazione add foreign key (ID_Firmatario) references Firmatario(ID_Firmatario) on delete cascade;

-- alter table Composizione add constraint FKCom_TRA_FK
--      foreign key (ID_Traccia)
--      references TRACCIA;

alter table Composizione add foreign key (ID_Traccia) references Traccia(ID_TRACCIA) on delete cascade;

-- alter table Composizione add constraint FKCom_ALB
--      foreign key (ID_Album)
--      references ALBUM;

alter table Composizione add foreign key (ID_Album) references Album(ID_Album) on delete cascade;

-- alter table CONCERTO add constraint FKOspita_FK
--      foreign key (ID_Luogo)
--      references LUOGO;

alter table Concerto add foreign key (ID_Luogo) references Luogo(ID_Luogo) on delete cascade;

-- alter table CONCERTO add constraint FKAppartenenza_FK
--      foreign key (ID_Tour)
--      references TOUR;

alter table Concerto add foreign key (ID_Tour) references Tour(ID_Tour) on delete cascade;

-- alter table CONTRATTO add constraint FKFirma_FK
--      foreign key (ID_Firmatario)
--      references FIRMATARIO;

alter table Contratto add foreign key (ID_Firmatario) references Firmatario(ID_Firmatario) on delete cascade;

-- alter table Feature add constraint FKFea_TRA
--      foreign key (ID_Traccia)
--      references TRACCIA;

alter table Feature add foreign key (ID_Traccia) references Traccia(ID_Traccia) on delete cascade;

-- alter table Feature add constraint FKFea_PRO_FK
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table Feature add foreign key (ID_Progetto) references Progetto_Musicale(ID_Progetto) on delete cascade;

-- alter table FornituraMerch add constraint FKFor_PRO_2_FK
--      foreign key (ID_Produttore)
--      references PRODUTTORE;

-- alter table FornituraMerch add constraint FKFor_MER_FK
--      foreign key (Codice)
--      references MERCHANDISING;

-- alter table FornituraProdotto add constraint FKFor_PRO_1_FK
--      foreign key (Codice)
--      references PRODOTTO;

-- alter table FornituraProdotto add constraint FKFor_PRO_FK
--      foreign key (ID_Produttore)
--      references PRODUTTORE;

-- alter table MERCHANDISING add constraint FKCreazione_FK
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table Merchandising add foreign key (ID_Progetto) references Progetto_Musicale(ID_Progetto) on delete cascade;

alter table Merchandising add foreign key (ID_Produttore) references Produttore(ID_Produttore) on delete cascade;

-- alter table Partecipazione add constraint FKPar_PRO
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table Partecipazione add foreign key (ID_Progetto) references Progetto_Musicale(ID_Progetto) on delete cascade;

-- alter table Partecipazione add constraint FKPar_FIR_FK
--      foreign key (ID_Firmatario)
--      references FIRMATARIO;

alter table Partecipazione add foreign key (ID_Firmatario) references Firmatario(ID_Firmatario) on delete cascade;

-- alter table Performance add constraint FKPer_PRO_FK
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table Performance add foreign key (ID_Progetto) references Progetto_Musicale(ID_Progetto) on delete cascade;

-- alter table Performance add constraint FKPer_CON
--      foreign key (ID_Concerto)
--      references CONCERTO;

alter table Performance add foreign key (ID_Concerto) references Concerto(ID_Concerto) on delete cascade;

-- alter table PRODOTTO add constraint FKEdizioneFisica_FK
--      foreign key (ID_Album)
--      references ALBUM;

alter table Prodotto add foreign key (ID_Album) references Album(ID_Album) on delete cascade;

alter table Prodotto add foreign key (ID_Produttore) references Produttore(ID_Produttore) on delete cascade;

-- alter table TRACCIA add constraint FKScrittura_Traccia
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table Traccia add foreign key (ID_Progetto) references Progetto_Musicale(ID_Progetto) on delete cascade;

-- Index Section
-- _____________ 

create unique index ID_ALBUM_IND
     on Album (ID_Album);

create unique index SID_ALBUM_IND
     on Album (ID_Progetto, Nome);

create unique index ID_BIGLIETTO_IND
     on Biglietto (ID_Biglietto);

create index FKDisposizione_IND
     on Biglietto (ID_Concerto);

create unique index ID_Collaborazione_IND
     on Collaborazione (ID_Traccia, ID_Firmatario);

create index FKCol_FIR_IND
     on Collaborazione (ID_Firmatario);

create unique index ID_Composizione_IND
     on Composizione (ID_Album, ID_Traccia);

create index FKCom_TRA_IND
     on Composizione (ID_Traccia);

create unique index ID_CONCERTO_IND
     on Concerto (ID_Concerto);

create index FKOspita_IND
     on Concerto (ID_Luogo);

create index FKAppartenenza_IND
     on Concerto (ID_Tour);

create unique index ID_CONTRATTO_IND
     on Contratto (Codice);

create index FKFirma_IND
     on Contratto (ID_Firmatario);

create unique index ID_Feature_IND
     on Feature (ID_Traccia, ID_Progetto);

create index FKFea_PRO_IND
     on Feature (ID_Progetto);

create unique index ID_FIRMATARIO_IND
     on Firmatario (ID_Firmatario);

create unique index SID_FIRMATARIO_IND
     on Firmatario (CF);

create unique index ID_LUOGO_IND
     on Luogo (ID_Luogo);

create unique index ID_MERCHANDISING_IND
     on Merchandising (Codice);

create index FKCreazione_IND
     on Merchandising (ID_Progetto);

create index FKFornituraMerch_IND
     on Merchandising (ID_Produttore);

create unique index ID_Partecipazione_IND
     on Partecipazione (ID_Progetto, ID_Firmatario);

create index FKPar_FIR_IND
     on Partecipazione (ID_Firmatario);

create unique index ID_Performance_IND
     on Performance (ID_Concerto, ID_Progetto);

create index FKPer_PRO_IND
     on Performance (ID_Progetto);

create unique index ID_PRODOTTO_IND
     on Prodotto (Codice);

create index FKEdizioneFisica_IND
     on Prodotto (ID_Album);

create index FKFornituraProdotto_IND
     on Prodotto (ID_Produttore);

create unique index ID_PRODUTTORE_IND
     on Produttore (ID_Produttore);

create unique index SID_PRODUTTORE_IND
     on Produttore (PIVA);

create unique index ID_PROGETTO_MUSICALE_IND
     on Progetto_Musicale (ID_Progetto);

create unique index ID_TOUR_IND
     on Tour (ID_Tour);

create unique index ID_TRACCIA_IND
     on Traccia (ID_Traccia);

create unique index SID_TRACCIA_IND
     on Traccia (ID_Progetto, Nome);
