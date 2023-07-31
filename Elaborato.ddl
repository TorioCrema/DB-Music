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

create table ALBUM (
     ID_Album numeric(10) not null,
     Nome varchar(50) not null,
     ID_Progetto numeric(10) not null,
     Durata numeric(5) not null,
     DataPubblicazione date not null,
     constraint ID_ALBUM_ID primary key (ID_Album),
     constraint SID_ALBUM_ID unique (ID_Progetto, Nome));

create table BIGLIETTO (
     ID_Biglietto numeric(10) not null,
     Costo numeric(4,2) not null,
     Descrizione varchar(50) not null,
     DispTot numeric(5) not null,
     NumVenduti numeric(5) not null,
     ID_Concerto numeric(10) not null,
     constraint ID_BIGLIETTO_ID primary key (ID_Biglietto));

create table Collaborazione (
     ID_Firmatario numeric(10) not null,
     ID_Traccia numeric(10) not null,
     constraint ID_Collaborazione_ID primary key (ID_Traccia, ID_Firmatario));

create table Composizione (
     ID_Album numeric(10) not null,
     ID_Traccia numeric(10) not null,
     constraint ID_Composizione_ID primary key (ID_Album, ID_Traccia));

create table CONCERTO (
     ID_Concerto numeric(10) not null,
     Data date not null,
     ID_Luogo numeric(10) not null,
     ID_Tour numeric(10) not null,
     constraint ID_CONCERTO_ID primary key (ID_Concerto));

create table CONTRATTO (
     Codice numeric(20) not null,
     Importo numeric(10,2) not null,
     DataInizio date not null,
     DataFine date not null,
     ID_Firmatario numeric(10) not null,
     constraint ID_CONTRATTO_ID primary key (Codice));

create table Feature (
     ID_Progetto numeric(10) not null,
     ID_Traccia numeric(10) not null,
     constraint ID_Feature_ID primary key (ID_Traccia, ID_Progetto));

create table FIRMATARIO (
     ID_Firmatario numeric(10) not null,
     CF char(16) not null,
     Nome varchar(20) not null,
     Cognome varchar(20) not null,
     Ind_NumCivico numeric(4) not null,
     Ind_Via varchar(50) not null,
     Ind_Citta varchar(50) not null,
     Ruolo varchar(20),
     constraint ID_FIRMATARIO_ID primary key (ID_Firmatario),
     constraint SID_FIRMATARIO_ID unique (CF));

create table FornituraMerch (
     Codice numeric(10) not null,
     QtaProdotta numeric(4) not null,
     PrezzoFornituraUnitario numeric(3) not null,
     ID_Produttore numeric(10) not null,
     constraint FKFor_MER_ID primary key (Codice));

create table FornituraProdotto (
     Codice numeric(10) not null,
     Costo numeric(7,2) not null,
     ID_Produttore numeric(10) not null,
     constraint FKFor_PRO_1_ID primary key (Codice));

create table LUOGO (
     ID_Luogo numeric(10) not null,
     Nome varchar(20) not null,
     Capienza numeric(5) not null,
     constraint ID_LUOGO_ID primary key (ID_Luogo));

create table MERCHANDISING (
     Codice numeric(10) not null,
     Descrizione varchar(50) not null,
     Prezzo numeric(4,2) not null,
     ID_Progetto numeric(10) not null,
     constraint ID_MERCHANDISING_ID primary key (Codice));

create table Partecipazione (
     ID_Firmatario numeric(10) not null,
     ID_Progetto numeric(10) not null,
     constraint ID_Partecipazione_ID primary key (ID_Progetto, ID_Firmatario));

create table Performance (
     ID_Concerto numeric(10) not null,
     ID_Progetto numeric(10) not null,
     constraint ID_Performance_ID primary key (ID_Concerto, ID_Progetto));

create table PRODOTTO (
     Codice numeric(10) not null,
     Nome varchar(20) not null,
     Descrizione varchar(50) not null,
     Prezzo numeric(4,2) not null,
     QtaProdotta numeric(4) not null,
     DataUscita date not null,
     Tipo varchar(10) not null,
     Formato numeric(2),
     ID_Album numeric(10) not null,
     constraint ID_PRODOTTO_ID primary key (Codice));

create table PRODUTTORE (
     ID_Produttore numeric(10) not null,
     PIVA char(11) not null,
     Nome varchar(20) not null,
     Ind_NumCivico numeric(4) not null,
     Ind_Via varchar(50) not null,
     Ind_Citta varchar(50) not null,
     NumForniture numeric(4) not null,
     constraint ID_PRODUTTORE_ID primary key (ID_Produttore),
     constraint SID_PRODUTTORE_ID unique (PIVA));

create table PROGETTO_MUSICALE (
     ID_Progetto numeric(10) not null,
     Nome varchar(20) not null,
     DataFormazione date not null,
     DataScioglimento date,
     Tipo varchar(20) not null,
     NumMembri numeric(2),
     constraint ID_PROGETTO_MUSICALE_ID primary key (ID_Progetto));

create table TOUR (
     ID_Tour numeric(10) not null,
     Nome varchar(20) not null,
     constraint ID_TOUR_ID primary key (ID_Tour));

create table TRACCIA (
     ID_Traccia numeric(10) not null,
     Nome varchar(20) not null,
     ID_Progetto numeric(10) not null,
     DataPubblicazione date not null,
     Durata numeric(4) not null,
     Testo varchar(10000),
     constraint ID_TRACCIA_ID primary key (ID_Traccia),
     constraint SID_TRACCIA_ID unique (ID_Progetto, Nome));


-- Constraints Section
-- ___________________ 

-- alter table ALBUM add constraint FKScrittura_Album
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table ALBUM add foreign key (ID_Progetto) references PROGETTO_MUSICALE(ID_Progetto);

-- alter table BIGLIETTO add constraint FKDisposizione_FK
--      foreign key (ID_Concerto)
--      references CONCERTO;

alter table BIGLIETTO add foreign key (ID_Concerto) references CONCERTO(ID_Concerto);

-- alter table Collaborazione add constraint FKCol_TRA
--      foreign key (ID_Traccia)
--      references TRACCIA;

alter table Collaborazione add foreign key (ID_Traccia) references TRACCIA(ID_TRACCIA);

-- alter table Collaborazione add constraint FKCol_FIR_FK
--      foreign key (ID_Firmatario)
--      references FIRMATARIO;

alter table Collaborazione add foreign key (ID_Firmatario) references FIRMATARIO(ID_Firmatario);

-- alter table Composizione add constraint FKCom_TRA_FK
--      foreign key (ID_Traccia)
--      references TRACCIA;

alter table Composizione add foreign key (ID_Traccia) references TRACCIA(ID_TRACCIA);

-- alter table Composizione add constraint FKCom_ALB
--      foreign key (ID_Album)
--      references ALBUM;

alter table Composizione add foreign key (ID_Album) references ALBUM(ID_Album);

-- alter table CONCERTO add constraint FKOspita_FK
--      foreign key (ID_Luogo)
--      references LUOGO;

alter table CONCERTO add foreign key (ID_Luogo) references LUOGO(ID_Luogo);

-- alter table CONCERTO add constraint FKAppartenenza_FK
--      foreign key (ID_Tour)
--      references TOUR;

alter table CONCERTO add foreign key (ID_Tour) references TOUR(ID_Tour);

-- alter table CONTRATTO add constraint FKFirma_FK
--      foreign key (ID_Firmatario)
--      references FIRMATARIO;

alter table CONTRATTO add foreign key (ID_Firmatario) references FIRMATARIO(ID_Firmatario);

-- alter table Feature add constraint FKFea_TRA
--      foreign key (ID_Traccia)
--      references TRACCIA;

alter table Feature add foreign key (ID_Traccia) references TRACCIA(ID_Traccia);

-- alter table Feature add constraint FKFea_PRO_FK
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table Feature add foreign key (ID_Progetto) references PROGETTO_MUSICALE(ID_Progetto);

-- alter table FornituraMerch add constraint FKFor_PRO_2_FK
--      foreign key (ID_Produttore)
--      references PRODUTTORE;

alter table FornituraMerch add foreign key (ID_Produttore) references PRODUTTORE(ID_Produttore);

-- alter table FornituraMerch add constraint FKFor_MER_FK
--      foreign key (Codice)
--      references MERCHANDISING;

alter table FornituraMerch add foreign key (Codice) references MERCHANDISING(Codice);

-- alter table FornituraProdotto add constraint FKFor_PRO_1_FK
--      foreign key (Codice)
--      references PRODOTTO;

alter table FornituraProdotto add foreign key (Codice) references PRODOTTO(Codice);

-- alter table FornituraProdotto add constraint FKFor_PRO_FK
--      foreign key (ID_Produttore)
--      references PRODUTTORE;

alter table FornituraProdotto add foreign key (ID_Produttore) references PRODUTTORE(ID_Produttore);

-- alter table MERCHANDISING add constraint FKCreazione_FK
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table MERCHANDISING add foreign key (ID_Progetto) references PROGETTO_MUSICALE(ID_Progetto);

-- alter table Partecipazione add constraint FKPar_PRO
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table Partecipazione add foreign key (ID_Progetto) references PROGETTO_MUSICALE(ID_Progetto);

-- alter table Partecipazione add constraint FKPar_FIR_FK
--      foreign key (ID_Firmatario)
--      references FIRMATARIO;

alter table Partecipazione add foreign key (ID_Firmatario) references FIRMATARIO(ID_Firmatario);

-- alter table Performance add constraint FKPer_PRO_FK
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table Performance add foreign key (ID_Progetto) references PROGETTO_MUSICALE(ID_Progetto);

-- alter table Performance add constraint FKPer_CON
--      foreign key (ID_Concerto)
--      references CONCERTO;

alter table Performance add foreign key (ID_Concerto) references CONCERTO(ID_Concerto);

-- alter table PRODOTTO add constraint FKEdizioneFisica_FK
--      foreign key (ID_Album)
--      references ALBUM;

alter table PRODOTTO add foreign key (ID_Album) references ALBUM(ID_Album);

-- alter table TRACCIA add constraint FKScrittura_Traccia
--      foreign key (ID_Progetto)
--      references PROGETTO_MUSICALE;

alter table TRACCIA add foreign key (ID_Progetto) references PROGETTO_MUSICALE(ID_Progetto);

-- Index Section
-- _____________ 

create unique index ID_ALBUM_IND
     on ALBUM (ID_Album);

create unique index SID_ALBUM_IND
     on ALBUM (ID_Progetto, Nome);

create unique index ID_BIGLIETTO_IND
     on BIGLIETTO (ID_Biglietto);

create index FKDisposizione_IND
     on BIGLIETTO (ID_Concerto);

create unique index ID_Collaborazione_IND
     on Collaborazione (ID_Traccia, ID_Firmatario);

create index FKCol_FIR_IND
     on Collaborazione (ID_Firmatario);

create unique index ID_Composizione_IND
     on Composizione (ID_Album, ID_Traccia);

create index FKCom_TRA_IND
     on Composizione (ID_Traccia);

create unique index ID_CONCERTO_IND
     on CONCERTO (ID_Concerto);

create index FKOspita_IND
     on CONCERTO (ID_Luogo);

create index FKAppartenenza_IND
     on CONCERTO (ID_Tour);

create unique index ID_CONTRATTO_IND
     on CONTRATTO (Codice);

create index FKFirma_IND
     on CONTRATTO (ID_Firmatario);

create unique index ID_Feature_IND
     on Feature (ID_Traccia, ID_Progetto);

create index FKFea_PRO_IND
     on Feature (ID_Progetto);

create unique index ID_FIRMATARIO_IND
     on FIRMATARIO (ID_Firmatario);

create unique index SID_FIRMATARIO_IND
     on FIRMATARIO (CF);

create index FKFor_PRO_2_IND
     on FornituraMerch (ID_Produttore);

create unique index FKFor_MER_IND
     on FornituraMerch (Codice);

create unique index FKFor_PRO_1_IND
     on FornituraProdotto (Codice);

create index FKFor_PRO_IND
     on FornituraProdotto (ID_Produttore);

create unique index ID_LUOGO_IND
     on LUOGO (ID_Luogo);

create unique index ID_MERCHANDISING_IND
     on MERCHANDISING (Codice);

create index FKCreazione_IND
     on MERCHANDISING (ID_Progetto);

create unique index ID_Partecipazione_IND
     on Partecipazione (ID_Progetto, ID_Firmatario);

create index FKPar_FIR_IND
     on Partecipazione (ID_Firmatario);

create unique index ID_Performance_IND
     on Performance (ID_Concerto, ID_Progetto);

create index FKPer_PRO_IND
     on Performance (ID_Progetto);

create unique index ID_PRODOTTO_IND
     on PRODOTTO (Codice);

create index FKEdizioneFisica_IND
     on PRODOTTO (ID_Album);

create unique index ID_PRODUTTORE_IND
     on PRODUTTORE (ID_Produttore);

create unique index SID_PRODUTTORE_IND
     on PRODUTTORE (PIVA);

create unique index ID_PROGETTO_MUSICALE_IND
     on PROGETTO_MUSICALE (ID_Progetto);

create unique index ID_TOUR_IND
     on TOUR (ID_Tour);

create unique index ID_TRACCIA_IND
     on TRACCIA (ID_Traccia);

create unique index SID_TRACCIA_IND
     on TRACCIA (ID_Progetto, Nome);

