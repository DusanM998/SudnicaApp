﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sudnica_API.DbContexts;

#nullable disable

namespace Sudnica_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230823072818_KreiranjeTabela")]
    partial class KreiranjeTabela
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Sudnica_API.Models.Kompanija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kompanije");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Adresa = "Redmond, WA 98052-6399 USA",
                            Naziv = "Microsoft"
                        },
                        new
                        {
                            Id = 2,
                            Adresa = "Market Square, 1355 Market St suite 900, San Francisco, CA 94103, USA",
                            Naziv = "Twitter"
                        },
                        new
                        {
                            Id = 3,
                            Adresa = "1 Hacker Way, Menlo Park, CA 94025, USA",
                            Naziv = "Meta"
                        },
                        new
                        {
                            Id = 4,
                            Adresa = "1 North Castle Drive, Armonk, NY 10504, USA",
                            Naziv = "IBM"
                        },
                        new
                        {
                            Id = 5,
                            Adresa = "One Apple Park Way; Cupertino, CA 95014, USA",
                            Naziv = "Apple"
                        },
                        new
                        {
                            Id = 6,
                            Adresa = "Round Rock, Texas, 78682, USA",
                            Naziv = "Dell"
                        });
                });

            modelBuilder.Entity("Sudnica_API.Models.Kontakt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KompanijaId")
                        .HasColumnType("int");

                    b.Property<bool>("PravnoFizickoLice")
                        .HasColumnType("bit");

                    b.Property<string>("Telefon1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zanimanje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KompanijaId");

                    b.ToTable("Kontakti");
                });

            modelBuilder.Entity("Sudnica_API.Models.Korisnik", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Godine")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PunoIme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Korisnik");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Sudnica_API.Models.Lokacija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lokacije");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Naslov = "Nis"
                        },
                        new
                        {
                            Id = 2,
                            Naslov = "Beograd"
                        },
                        new
                        {
                            Id = 3,
                            Naslov = "Novi Sad"
                        });
                });

            modelBuilder.Entity("Sudnica_API.Models.Parnica", b =>
                {
                    b.Property<int>("ParnicaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParnicaId"));

                    b.Property<int>("BrojSudnice")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumOdrzavanja")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentifikatorPostupka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LokacijaId")
                        .HasColumnType("int");

                    b.Property<string>("Napomena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SudijaId")
                        .HasColumnType("int");

                    b.Property<int>("TipPostupkaId")
                        .HasColumnType("int");

                    b.Property<string>("TipUstanove")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TuzenikId")
                        .HasColumnType("int");

                    b.Property<int>("TuzilacId")
                        .HasColumnType("int");

                    b.HasKey("ParnicaId");

                    b.HasIndex("LokacijaId");

                    b.HasIndex("SudijaId");

                    b.HasIndex("TipPostupkaId");

                    b.HasIndex("TuzenikId");

                    b.HasIndex("TuzilacId");

                    b.ToTable("Parnice");
                });

            modelBuilder.Entity("Sudnica_API.Models.TipPostupka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoviPostupaka");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Naslov = "Parnični postupak"
                        },
                        new
                        {
                            Id = 2,
                            Naslov = "Vanparnični postupak"
                        },
                        new
                        {
                            Id = 3,
                            Naslov = "Krivični postupak"
                        },
                        new
                        {
                            Id = 4,
                            Naslov = "Izvršni postupak"
                        });
                });

            modelBuilder.Entity("Sudnica_API_Test.Models.KorisnikParnica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ParnicaId")
                        .HasColumnType("int");

                    b.Property<string>("ZaduzeniAdvokatId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ParnicaId");

                    b.HasIndex("ZaduzeniAdvokatId");

                    b.ToTable("KorisniciParnice");
                });

            modelBuilder.Entity("Sudnica_API.Models.ZaduzeniAdvokat", b =>
                {
                    b.HasBaseType("Sudnica_API.Models.Korisnik");

                    b.Property<int>("ParnicaId")
                        .HasColumnType("int");

                    b.HasIndex("ParnicaId");

                    b.HasDiscriminator().HasValue("ZaduzeniAdvokat");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Sudnica_API.Models.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Sudnica_API.Models.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sudnica_API.Models.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Sudnica_API.Models.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sudnica_API.Models.Kontakt", b =>
                {
                    b.HasOne("Sudnica_API.Models.Kompanija", "PripadnostKompaniji")
                        .WithMany()
                        .HasForeignKey("KompanijaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PripadnostKompaniji");
                });

            modelBuilder.Entity("Sudnica_API.Models.Parnica", b =>
                {
                    b.HasOne("Sudnica_API.Models.Lokacija", "Lokacija")
                        .WithMany()
                        .HasForeignKey("LokacijaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sudnica_API.Models.Kontakt", "Sudija")
                        .WithMany()
                        .HasForeignKey("SudijaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sudnica_API.Models.TipPostupka", "TipPostupka")
                        .WithMany()
                        .HasForeignKey("TipPostupkaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sudnica_API.Models.Kontakt", "Tuzenik")
                        .WithMany()
                        .HasForeignKey("TuzenikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sudnica_API.Models.Kontakt", "Tuzilac")
                        .WithMany()
                        .HasForeignKey("TuzilacId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Lokacija");

                    b.Navigation("Sudija");

                    b.Navigation("TipPostupka");

                    b.Navigation("Tuzenik");

                    b.Navigation("Tuzilac");
                });

            modelBuilder.Entity("Sudnica_API_Test.Models.KorisnikParnica", b =>
                {
                    b.HasOne("Sudnica_API.Models.Parnica", "Parnica")
                        .WithMany()
                        .HasForeignKey("ParnicaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sudnica_API.Models.ZaduzeniAdvokat", "ZaduzeniAdvokat")
                        .WithMany()
                        .HasForeignKey("ZaduzeniAdvokatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Parnica");

                    b.Navigation("ZaduzeniAdvokat");
                });

            modelBuilder.Entity("Sudnica_API.Models.ZaduzeniAdvokat", b =>
                {
                    b.HasOne("Sudnica_API.Models.Parnica", null)
                        .WithMany("ZaduzeniAdvokati")
                        .HasForeignKey("ParnicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sudnica_API.Models.Parnica", b =>
                {
                    b.Navigation("ZaduzeniAdvokati");
                });
#pragma warning restore 612, 618
        }
    }
}
