using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WpfDBMusicLabel.musiclabeldb;

public partial class MusiclabeldbContext : DbContext
{
    public MusiclabeldbContext()
    {
    }

    public MusiclabeldbContext(DbContextOptions<MusiclabeldbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Biglietto> Bigliettos { get; set; }

    public virtual DbSet<Concerto> Concertos { get; set; }

    public virtual DbSet<Contratto> Contrattos { get; set; }

    public virtual DbSet<Firmatario> Firmatarios { get; set; }

    public virtual DbSet<Luogo> Luogos { get; set; }

    public virtual DbSet<Merchandising> Merchandisings { get; set; }

    public virtual DbSet<Prodotto> Prodottos { get; set; }

    public virtual DbSet<Produttore> Produttores { get; set; }

    public virtual DbSet<ProgettoMusicale> ProgettoMusicales { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<Traccium> Traccia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("SERVER=localhost; PORT=3306; DATABASE=musiclabeldb; UID=root; PASSWORD=73a6RV+(");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.IdAlbum).HasName("PRIMARY");

            entity.ToTable("album");

            entity.HasIndex(e => e.IdAlbum, "ID_ALBUM_IND").IsUnique();

            entity.HasIndex(e => new { e.IdProgetto, e.Nome }, "SID_ALBUM_ID").IsUnique();

            entity.HasIndex(e => new { e.IdProgetto, e.Nome }, "SID_ALBUM_IND").IsUnique();

            entity.Property(e => e.IdAlbum).HasColumnName("ID_Album");
            entity.Property(e => e.DataPubblicazione).HasColumnType("date");
            entity.Property(e => e.IdProgetto).HasColumnName("ID_Progetto");
            entity.Property(e => e.Nome).HasMaxLength(50);

            entity.HasOne(d => d.IdProgettoNavigation).WithMany(p => p.Albums)
                .HasForeignKey(d => d.IdProgetto)
                .HasConstraintName("album_ibfk_1");

            entity.HasMany(d => d.IdTraccia).WithMany(p => p.IdAlbums)
                .UsingEntity<Dictionary<string, object>>(
                    "Composizione",
                    r => r.HasOne<Traccium>().WithMany()
                        .HasForeignKey("IdTraccia")
                        .HasConstraintName("composizione_ibfk_1"),
                    l => l.HasOne<Album>().WithMany()
                        .HasForeignKey("IdAlbum")
                        .HasConstraintName("composizione_ibfk_2"),
                    j =>
                    {
                        j.HasKey("IdAlbum", "IdTraccia").HasName("PRIMARY");
                        j.ToTable("composizione");
                        j.HasIndex(new[] { "IdTraccia" }, "FKCom_TRA_IND");
                        j.HasIndex(new[] { "IdAlbum", "IdTraccia" }, "ID_Composizione_IND").IsUnique();
                        j.IndexerProperty<uint>("IdAlbum").HasColumnName("ID_Album");
                        j.IndexerProperty<uint>("IdTraccia").HasColumnName("ID_Traccia");
                    });
        });

        modelBuilder.Entity<Biglietto>(entity =>
        {
            entity.HasKey(e => e.IdBiglietto).HasName("PRIMARY");

            entity.ToTable("biglietto");

            entity.HasIndex(e => e.IdConcerto, "FKDisposizione_IND");

            entity.HasIndex(e => e.IdBiglietto, "ID_BIGLIETTO_IND").IsUnique();

            entity.Property(e => e.IdBiglietto).HasColumnName("ID_Biglietto");
            entity.Property(e => e.Descrizione).HasMaxLength(50);
            entity.Property(e => e.IdConcerto).HasColumnName("ID_Concerto");

            entity.HasOne(d => d.IdConcertoNavigation).WithMany(p => p.Bigliettos)
                .HasForeignKey(d => d.IdConcerto)
                .HasConstraintName("biglietto_ibfk_1");
        });

        modelBuilder.Entity<Concerto>(entity =>
        {
            entity.HasKey(e => e.IdConcerto).HasName("PRIMARY");

            entity.ToTable("concerto");

            entity.HasIndex(e => e.IdTour, "FKAppartenenza_IND");

            entity.HasIndex(e => e.IdLuogo, "FKOspita_IND");

            entity.HasIndex(e => e.IdConcerto, "ID_CONCERTO_IND").IsUnique();

            entity.Property(e => e.IdConcerto).HasColumnName("ID_Concerto");
            entity.Property(e => e.Data).HasColumnType("date");
            entity.Property(e => e.IdLuogo).HasColumnName("ID_Luogo");
            entity.Property(e => e.IdTour).HasColumnName("ID_Tour");

            entity.HasOne(d => d.IdLuogoNavigation).WithMany(p => p.Concertos)
                .HasForeignKey(d => d.IdLuogo)
                .HasConstraintName("concerto_ibfk_1");

            entity.HasOne(d => d.IdTourNavigation).WithMany(p => p.Concertos)
                .HasForeignKey(d => d.IdTour)
                .HasConstraintName("concerto_ibfk_2");

            entity.HasMany(d => d.IdProgettos).WithMany(p => p.IdConcertos)
                .UsingEntity<Dictionary<string, object>>(
                    "Performance",
                    r => r.HasOne<ProgettoMusicale>().WithMany()
                        .HasForeignKey("IdProgetto")
                        .HasConstraintName("performance_ibfk_1"),
                    l => l.HasOne<Concerto>().WithMany()
                        .HasForeignKey("IdConcerto")
                        .HasConstraintName("performance_ibfk_2"),
                    j =>
                    {
                        j.HasKey("IdConcerto", "IdProgetto").HasName("PRIMARY");
                        j.ToTable("performance");
                        j.HasIndex(new[] { "IdProgetto" }, "FKPer_PRO_IND");
                        j.HasIndex(new[] { "IdConcerto", "IdProgetto" }, "ID_Performance_IND").IsUnique();
                        j.IndexerProperty<uint>("IdConcerto").HasColumnName("ID_Concerto");
                        j.IndexerProperty<uint>("IdProgetto").HasColumnName("ID_Progetto");
                    });
        });

        modelBuilder.Entity<Contratto>(entity =>
        {
            entity.HasKey(e => e.Codice).HasName("PRIMARY");

            entity.ToTable("contratto");

            entity.HasIndex(e => e.IdFirmatario, "FKFirma_IND");

            entity.HasIndex(e => e.Codice, "ID_CONTRATTO_IND").IsUnique();

            entity.Property(e => e.DataFine).HasColumnType("date");
            entity.Property(e => e.DataInizio).HasColumnType("date");
            entity.Property(e => e.IdFirmatario).HasColumnName("ID_Firmatario");

            entity.HasOne(d => d.IdFirmatarioNavigation).WithMany(p => p.Contrattos)
                .HasForeignKey(d => d.IdFirmatario)
                .HasConstraintName("contratto_ibfk_1");
        });

        modelBuilder.Entity<Firmatario>(entity =>
        {
            entity.HasKey(e => e.IdFirmatario).HasName("PRIMARY");

            entity.ToTable("firmatario");

            entity.HasIndex(e => e.IdFirmatario, "ID_FIRMATARIO_IND").IsUnique();

            entity.HasIndex(e => e.Cf, "SID_FIRMATARIO_ID").IsUnique();

            entity.HasIndex(e => e.Cf, "SID_FIRMATARIO_IND").IsUnique();

            entity.Property(e => e.IdFirmatario).HasColumnName("ID_Firmatario");
            entity.Property(e => e.Cf)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("CF");
            entity.Property(e => e.Cognome).HasMaxLength(50);
            entity.Property(e => e.IndCitta)
                .HasMaxLength(50)
                .HasColumnName("Ind_Citta");
            entity.Property(e => e.IndNumCivico).HasColumnName("Ind_NumCivico");
            entity.Property(e => e.IndVia)
                .HasMaxLength(50)
                .HasColumnName("Ind_Via");
            entity.Property(e => e.Nome).HasMaxLength(50);
            entity.Property(e => e.Ruolo).HasMaxLength(50);
        });

        modelBuilder.Entity<Luogo>(entity =>
        {
            entity.HasKey(e => e.IdLuogo).HasName("PRIMARY");

            entity.ToTable("luogo");

            entity.HasIndex(e => e.IdLuogo, "ID_LUOGO_IND").IsUnique();

            entity.Property(e => e.IdLuogo).HasColumnName("ID_Luogo");
            entity.Property(e => e.Nome).HasMaxLength(50);
        });

        modelBuilder.Entity<Merchandising>(entity =>
        {
            entity.HasKey(e => e.Codice).HasName("PRIMARY");

            entity.ToTable("merchandising");

            entity.HasIndex(e => e.IdProgetto, "FKCreazione_IND");

            entity.HasIndex(e => e.IdProduttore, "FKFornituraMerch_IND");

            entity.HasIndex(e => e.Codice, "ID_MERCHANDISING_IND").IsUnique();

            entity.Property(e => e.Descrizione).HasMaxLength(50);
            entity.Property(e => e.IdProduttore).HasColumnName("ID_Produttore");
            entity.Property(e => e.IdProgetto).HasColumnName("ID_Progetto");

            entity.HasOne(d => d.IdProduttoreNavigation).WithMany(p => p.Merchandisings)
                .HasForeignKey(d => d.IdProduttore)
                .HasConstraintName("merchandising_ibfk_2");

            entity.HasOne(d => d.IdProgettoNavigation).WithMany(p => p.Merchandisings)
                .HasForeignKey(d => d.IdProgetto)
                .HasConstraintName("merchandising_ibfk_1");
        });

        modelBuilder.Entity<Prodotto>(entity =>
        {
            entity.HasKey(e => e.Codice).HasName("PRIMARY");

            entity.ToTable("prodotto");

            entity.HasIndex(e => e.IdAlbum, "FKEdizioneFisica_IND");

            entity.HasIndex(e => e.IdProduttore, "FKFornituraProdotto_IND");

            entity.HasIndex(e => e.Codice, "ID_PRODOTTO_IND").IsUnique();

            entity.Property(e => e.DataUscita).HasColumnType("date");
            entity.Property(e => e.Descrizione).HasMaxLength(50);
            entity.Property(e => e.IdAlbum).HasColumnName("ID_Album");
            entity.Property(e => e.IdProduttore).HasColumnName("ID_Produttore");
            entity.Property(e => e.Nome).HasMaxLength(50);
            entity.Property(e => e.Tipo).HasMaxLength(10);

            entity.HasOne(d => d.IdAlbumNavigation).WithMany(p => p.Prodottos)
                .HasForeignKey(d => d.IdAlbum)
                .HasConstraintName("prodotto_ibfk_1");

            entity.HasOne(d => d.IdProduttoreNavigation).WithMany(p => p.Prodottos)
                .HasForeignKey(d => d.IdProduttore)
                .HasConstraintName("prodotto_ibfk_2");
        });

        modelBuilder.Entity<Produttore>(entity =>
        {
            entity.HasKey(e => e.IdProduttore).HasName("PRIMARY");

            entity.ToTable("produttore");

            entity.HasIndex(e => e.IdProduttore, "ID_PRODUTTORE_IND").IsUnique();

            entity.HasIndex(e => e.Piva, "SID_PRODUTTORE_ID").IsUnique();

            entity.HasIndex(e => e.Piva, "SID_PRODUTTORE_IND").IsUnique();

            entity.Property(e => e.IdProduttore).HasColumnName("ID_Produttore");
            entity.Property(e => e.IndCitta)
                .HasMaxLength(50)
                .HasColumnName("Ind_Citta");
            entity.Property(e => e.IndNumCivico).HasColumnName("Ind_NumCivico");
            entity.Property(e => e.IndVia)
                .HasMaxLength(50)
                .HasColumnName("Ind_Via");
            entity.Property(e => e.Nome).HasMaxLength(50);
            entity.Property(e => e.Piva)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("PIVA");
        });

        modelBuilder.Entity<ProgettoMusicale>(entity =>
        {
            entity.HasKey(e => e.IdProgetto).HasName("PRIMARY");

            entity.ToTable("progetto_musicale");

            entity.HasIndex(e => e.IdProgetto, "ID_PROGETTO_MUSICALE_IND").IsUnique();

            entity.Property(e => e.IdProgetto).HasColumnName("ID_Progetto");
            entity.Property(e => e.DataFormazione).HasColumnType("date");
            entity.Property(e => e.DataScioglimento).HasColumnType("date");
            entity.Property(e => e.Nome).HasMaxLength(50);
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasMany(d => d.IdFirmatarios).WithMany(p => p.IdProgettos)
                .UsingEntity<Dictionary<string, object>>(
                    "Partecipazione",
                    r => r.HasOne<Firmatario>().WithMany()
                        .HasForeignKey("IdFirmatario")
                        .HasConstraintName("partecipazione_ibfk_2"),
                    l => l.HasOne<ProgettoMusicale>().WithMany()
                        .HasForeignKey("IdProgetto")
                        .HasConstraintName("partecipazione_ibfk_1"),
                    j =>
                    {
                        j.HasKey("IdProgetto", "IdFirmatario").HasName("PRIMARY");
                        j.ToTable("partecipazione");
                        j.HasIndex(new[] { "IdFirmatario" }, "FKPar_FIR_IND");
                        j.HasIndex(new[] { "IdProgetto", "IdFirmatario" }, "ID_Partecipazione_IND").IsUnique();
                        j.IndexerProperty<uint>("IdProgetto").HasColumnName("ID_Progetto");
                        j.IndexerProperty<uint>("IdFirmatario").HasColumnName("ID_Firmatario");
                    });
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.IdTour).HasName("PRIMARY");

            entity.ToTable("tour");

            entity.HasIndex(e => e.IdTour, "ID_TOUR_IND").IsUnique();

            entity.Property(e => e.IdTour).HasColumnName("ID_Tour");
            entity.Property(e => e.Nome).HasMaxLength(50);
        });

        modelBuilder.Entity<Traccium>(entity =>
        {
            entity.HasKey(e => e.IdTraccia).HasName("PRIMARY");

            entity.ToTable("traccia");

            entity.HasIndex(e => e.IdTraccia, "ID_TRACCIA_IND").IsUnique();

            entity.HasIndex(e => new { e.IdProgetto, e.Nome }, "SID_TRACCIA_ID").IsUnique();

            entity.HasIndex(e => new { e.IdProgetto, e.Nome }, "SID_TRACCIA_IND").IsUnique();

            entity.Property(e => e.IdTraccia).HasColumnName("ID_Traccia");
            entity.Property(e => e.DataPubblicazione).HasColumnType("date");
            entity.Property(e => e.IdProgetto).HasColumnName("ID_Progetto");
            entity.Property(e => e.Nome).HasMaxLength(50);
            entity.Property(e => e.Testo).HasMaxLength(10000);

            entity.HasOne(d => d.IdProgettoNavigation).WithMany(p => p.Traccia)
                .HasForeignKey(d => d.IdProgetto)
                .HasConstraintName("traccia_ibfk_1");

            entity.HasMany(d => d.IdFirmatarios).WithMany(p => p.IdTraccia)
                .UsingEntity<Dictionary<string, object>>(
                    "Collaborazione",
                    r => r.HasOne<Firmatario>().WithMany()
                        .HasForeignKey("IdFirmatario")
                        .HasConstraintName("collaborazione_ibfk_2"),
                    l => l.HasOne<Traccium>().WithMany()
                        .HasForeignKey("IdTraccia")
                        .HasConstraintName("collaborazione_ibfk_1"),
                    j =>
                    {
                        j.HasKey("IdTraccia", "IdFirmatario").HasName("PRIMARY");
                        j.ToTable("collaborazione");
                        j.HasIndex(new[] { "IdFirmatario" }, "FKCol_FIR_IND");
                        j.HasIndex(new[] { "IdTraccia", "IdFirmatario" }, "ID_Collaborazione_IND").IsUnique();
                        j.IndexerProperty<uint>("IdTraccia").HasColumnName("ID_Traccia");
                        j.IndexerProperty<uint>("IdFirmatario").HasColumnName("ID_Firmatario");
                    });

            entity.HasMany(d => d.IdProgettos).WithMany(p => p.IdTraccia)
                .UsingEntity<Dictionary<string, object>>(
                    "Feature",
                    r => r.HasOne<ProgettoMusicale>().WithMany()
                        .HasForeignKey("IdProgetto")
                        .HasConstraintName("feature_ibfk_2"),
                    l => l.HasOne<Traccium>().WithMany()
                        .HasForeignKey("IdTraccia")
                        .HasConstraintName("feature_ibfk_1"),
                    j =>
                    {
                        j.HasKey("IdTraccia", "IdProgetto").HasName("PRIMARY");
                        j.ToTable("feature");
                        j.HasIndex(new[] { "IdProgetto" }, "FKFea_PRO_IND");
                        j.HasIndex(new[] { "IdTraccia", "IdProgetto" }, "ID_Feature_IND").IsUnique();
                        j.IndexerProperty<uint>("IdTraccia").HasColumnName("ID_Traccia");
                        j.IndexerProperty<uint>("IdProgetto").HasColumnName("ID_Progetto");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
