Ai fini dell'esecuzione dell'applicazione è necessario avere installato Visual Studio 2022 e MySql.

Operazioni necessarie per il database:
1. Installare MySql.
2. Eseguire le istruzioni all'interno di "elaborato.sql" per creare il database e, successivamente "insert.sql" per
    popolarlo con dati di esempio

Operazioni necessarie per l'applicazione:
1. Una volta aperto il progetto tramite VS2022, è necessario installare da NuGet i seguenti package:
- CommunityToolkit.Mvvm
- EntityFramework
- Microsoft.EntityFrameworkCore.Tools
- MySql.Data
- MySql.EntityFrameworkCore
- Pomelo.EntityFrameworkCore.MySql

2. Scaricare da https://www.mysql.com/products/connector/ il connector "ADO.NET Driver for MySQL (Connector/NET)".
3. Inserire le credenziali di MySql nel metodo OnConfiguring() all'interno della classe MusiclabeldbContext.cs,
    specificando il server, la porta il database, l'ID e la password.
    es. protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseMySQL("SERVER=localhost; PORT=3306; DATABASE=musiclabeldb; UID=root; PASSWORD=sinio");