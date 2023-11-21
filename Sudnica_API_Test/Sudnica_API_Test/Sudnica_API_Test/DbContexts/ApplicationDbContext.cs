using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sudnica_API_Test.Models;
using SudnicaAPI_Test.Models;
using System.Reflection.Emit;

namespace SudnicaAPI_Test.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Kompanija> Kompanije { get; set; }
        public DbSet<Lokacija> Lokacije { get; set; }
        public DbSet<TipPostupka> TipoviPostupaka { get; set; }
        public DbSet<Kontakt> Kontakti { get; set; }
        public DbSet<Parnica> Parnice { get; set; }
        public DbSet<KorisnikParnica> KorisniciParnice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kompanija>().HasData(
                new Kompanija
                {
                    Id = 1,
                    Naziv = "Microsoft",
                    Adresa = "Redmond, WA 98052-6399 USA"
                },
                new Kompanija
                {
                    Id = 2,
                    Naziv = "Twitter",
                    Adresa = "Market Square, 1355 Market St suite 900, San Francisco, CA 94103, USA"
                },
                new Kompanija
                {
                    Id = 3,
                    Naziv = "Meta",
                    Adresa = "1 Hacker Way, Menlo Park, CA 94025, USA"
                },
                new Kompanija
                {
                    Id = 4,
                    Naziv = "IBM",
                    Adresa = "1 North Castle Drive, Armonk, NY 10504, USA"
                },
                new Kompanija
                {
                    Id = 5,
                    Naziv = "Apple",
                    Adresa = "One Apple Park Way; Cupertino, CA 95014, USA"
                },
                new Kompanija
                {
                    Id = 6,
                    Naziv = "Dell",
                    Adresa = "Round Rock, Texas, 78682, USA"
                });

            modelBuilder.Entity<Lokacija>().HasData(
                new Lokacija
                {
                    Id = 1,
                    Naslov = "Nis"
                },
                new Lokacija
                {
                    Id = 2,
                    Naslov = "Beograd"
                },
                new Lokacija
                {
                    Id = 3,
                    Naslov = "Novi Sad"
                });

            modelBuilder.Entity<TipPostupka>().HasData(
                new TipPostupka
                {
                    Id = 1,
                    Naslov = "Parnični postupak"
                },
                new TipPostupka
                {
                    Id = 2,
                    Naslov = "Vanparnični postupak"
                },
                new TipPostupka
                {
                    Id = 3,
                    Naslov = "Krivični postupak"
                },
                new TipPostupka
                {
                    Id = 4,
                    Naslov = "Izvršni postupak"
                });

            modelBuilder.Entity<Parnica>()
                .HasOne(s => s.Sudija)
                .WithMany()
                .HasForeignKey(p => p.SudijaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parnica>(entity =>
            {
                entity.HasOne(t => t.Tuzilac)
                .WithMany()
                .HasForeignKey(t => t.TuzilacId)
                .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Parnica>(entity =>
            {
                entity.HasOne(t => t.Tuzenik)
                .WithMany()
                .HasForeignKey(t => t.TuzenikId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<KorisnikParnica>()
                .HasOne(k => k.Korisnik)
                .WithMany()
                .HasForeignKey(k => k.KorisnikId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KorisnikParnica>()
                .HasOne(p => p.Parnica)
                .WithMany()
                .HasForeignKey(p => p.ParnicaId)
                .OnDelete(DeleteBehavior.Restrict);

            /*modelBuilder.Entity<Parnica>()
                .HasMany(e => e.ZaduzeniAdvokati)
                .WithOne(e => e.Parnica)
                .HasForeignKey(e => e.ParnicaId)
                .IsRequired();

            modelBuilder.Entity<Korisnik>()
                .HasOne(e => e.Parnica)
                .WithMany(e => e.ZaduzeniAdvokati)
                .HasForeignKey(e => e.ParnicaId)
                .IsRequired();*/
        }
    }
}
