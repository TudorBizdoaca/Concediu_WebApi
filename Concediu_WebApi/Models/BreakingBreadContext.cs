using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Concediu_WebApi.Models
{
    public partial class BreakingBreadContext : DbContext
    {
        public BreakingBreadContext()
        {
        }

        public BreakingBreadContext(DbContextOptions<BreakingBreadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Angajat> Angajats { get; set; } = null!;
        public virtual DbSet<Concediu> Concedius { get; set; } = null!;
        public virtual DbSet<StareConcediu> StareConcedius { get; set; } = null!;
        public virtual DbSet<TipConcediu> TipConcedius { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Angajat>(entity =>
            {
                entity.ToTable("Angajat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cnp)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("cnp");

                entity.Property(e => e.DataAngajare)
                    .HasColumnType("datetime")
                    .HasColumnName("dataAngajare");

                entity.Property(e => e.DataNasterii)
                    .HasColumnType("datetime")
                    .HasColumnName("dataNasterii");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EsteAdmin)
                    .HasColumnName("esteAdmin")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ManagerId).HasColumnName("managerId");

                entity.Property(e => e.No)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("no");

                entity.Property(e => e.NrTelefon)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nrTelefon");

                entity.Property(e => e.Nume)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nume");

                entity.Property(e => e.Parola)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("parola");

                entity.Ignore(e=>e.Poza);//Property(e => e.Poza).HasColumnName("poza"));

                entity.Property(e => e.Prenume)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prenume");

                entity.Property(e => e.Serie)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("serie");

                entity.Property(e => e.ZileConcediu).HasDefaultValueSql("((21))");

                entity.HasOne(d => d.Manager);
                    
            });

            modelBuilder.Entity<Concediu>(entity =>
            {
                entity.ToTable("Concediu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AngajatId).HasColumnName("angajatId");

                entity.Property(e => e.Comentarii).HasColumnName("comentarii");

                entity.Property(e => e.DataInceput)
                    .HasColumnType("datetime")
                    .HasColumnName("dataInceput");

                entity.Property(e => e.DataSfarsit)
                    .HasColumnType("datetime")
                    .HasColumnName("dataSfarsit");

                entity.Property(e => e.InlocuitorId).HasColumnName("inlocuitorId");

                entity.Property(e => e.StareConcediuId).HasColumnName("stareConcediuId");

                entity.Property(e => e.TipConcediuId).HasColumnName("tipConcediuId");

                entity.HasOne(d => d.Angajat)
                    .WithMany(p => p.ConcediuAngajats)
                    .HasForeignKey(d => d.AngajatId)
                    .HasConstraintName("FK__Concediu__angaja__4222D4EF");

                entity.HasOne(d => d.Inlocuitor);
                    

                entity.HasOne(d => d.StareConcediu)
                    .WithMany(p => p.Concedius)
                    .HasForeignKey(d => d.StareConcediuId)
                    .HasConstraintName("FK__Concediu__stareC__412EB0B6");

                entity.HasOne(d => d.TipConcediu)
                    .WithMany(p => p.Concedius)
                    .HasForeignKey(d => d.TipConcediuId)
                    .HasConstraintName("FK__Concediu__tipCon__3F466844");
            });

            modelBuilder.Entity<StareConcediu>(entity =>
            {
                entity.ToTable("StareConcediu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cod)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod");

                entity.Property(e => e.Nume)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nume");
            });

            modelBuilder.Entity<TipConcediu>(entity =>
            {
                entity.ToTable("TipConcediu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cod)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod");

                entity.Property(e => e.Nume)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nume");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
