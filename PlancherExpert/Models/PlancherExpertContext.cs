using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlancherExpert.Models;

public partial class PlancherExpertContext : DbContext
{
    public PlancherExpertContext()
    {
    }

    public PlancherExpertContext(DbContextOptions<PlancherExpertContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<CouvrePlancher> CouvrePlanchers { get; set; }

    public virtual DbSet<Superviseur> Superviseurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=PlancherExpertContextConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC07EB894376");

            entity.ToTable("Client");

            entity.Property(e => e.Adresse)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tel)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Commande__3214EC07AC03A117");

            entity.ToTable("Commande");

            entity.Property(e => e.TypePlancher)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK__Commande__IdClie__286302EC");
        });

        modelBuilder.Entity<CouvrePlancher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CouvrePl__3214EC0754E28EFF");

            entity.ToTable("CouvrePlancher");

            entity.Property(e => e.PrixMat).HasColumnName("prixMat");
            entity.Property(e => e.TypePlancher)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Superviseur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supervis__3214EC0746CB2334");

            entity.ToTable("Superviseur");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ville)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ZIP");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
