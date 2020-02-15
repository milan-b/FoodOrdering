﻿// <auto-generated />
using System;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(HranaContext))]
    partial class HranaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("BookId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("Domain.Models.Hrana", b =>
                {
                    b.Property<int>("HranaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Stalna")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("HranaId");

                    b.ToTable("Hrana");
                });

            modelBuilder.Entity("Domain.Models.HranaMeni", b =>
                {
                    b.Property<int>("HranaId")
                        .HasColumnType("int");

                    b.Property<int>("MeniId")
                        .HasColumnType("int");

                    b.HasKey("HranaId", "MeniId");

                    b.HasIndex("MeniId");

                    b.ToTable("HranaMeni");
                });

            modelBuilder.Entity("Domain.Models.HranaPrilog", b =>
                {
                    b.Property<int>("HranaId")
                        .HasColumnType("int");

                    b.Property<int>("PrilogId")
                        .HasColumnType("int");

                    b.HasKey("HranaId", "PrilogId");

                    b.HasIndex("PrilogId");

                    b.ToTable("HranaPrilog");
                });

            modelBuilder.Entity("Domain.Models.Komentar", b =>
                {
                    b.Property<int>("KomentarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HranaId")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<string>("Slika")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("KomentarId");

                    b.HasIndex("HranaId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Komentar");
                });

            modelBuilder.Entity("Domain.Models.Korisnik", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Lozinka")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Prezime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("KorisnikId");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("Domain.Models.Meni", b =>
                {
                    b.Property<int>("MeniId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("Date");

                    b.HasKey("MeniId");

                    b.HasIndex("Datum")
                        .IsUnique();

                    b.ToTable("Meni");
                });

            modelBuilder.Entity("Domain.Models.Narudzba", b =>
                {
                    b.Property<int>("NarudzbaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Dostavljena")
                        .HasColumnType("int");

                    b.Property<int>("HranaId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("MeniId")
                        .HasColumnType("int");

                    b.Property<int>("PrilogId")
                        .HasColumnType("int");

                    b.HasKey("NarudzbaId");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("MeniId");

                    b.ToTable("Narudzba");
                });

            modelBuilder.Entity("Domain.Models.Ocjena", b =>
                {
                    b.Property<int>("OcjenaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HranaId")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("Vrijednost")
                        .HasColumnType("int");

                    b.HasKey("OcjenaId");

                    b.HasIndex("HranaId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Ocjena");
                });

            modelBuilder.Entity("Domain.Models.Prilog", b =>
                {
                    b.Property<int>("PrilogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Varijanta")
                        .HasColumnType("int");

                    b.Property<string>("Vrijednost")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("PrilogId");

                    b.ToTable("Prilog");
                });

            modelBuilder.Entity("Domain.Models.SavedBook", b =>
                {
                    b.Property<int>("SavedBookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("SavedName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SavedBookId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("SavedBook");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Domain.Models.HranaMeni", b =>
                {
                    b.HasOne("Domain.Models.Hrana", "Hrana")
                        .WithMany("Menii")
                        .HasForeignKey("HranaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Meni", "Meni")
                        .WithMany("Hrana")
                        .HasForeignKey("MeniId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.HranaPrilog", b =>
                {
                    b.HasOne("Domain.Models.Hrana", "Hrana")
                        .WithMany("Prilozi")
                        .HasForeignKey("HranaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Prilog", "Prilog")
                        .WithMany("Hrana")
                        .HasForeignKey("PrilogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Komentar", b =>
                {
                    b.HasOne("Domain.Models.Hrana", null)
                        .WithMany("Komentari")
                        .HasForeignKey("HranaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Narudzba", b =>
                {
                    b.HasOne("Domain.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Meni", null)
                        .WithMany("Narudzbe")
                        .HasForeignKey("MeniId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Ocjena", b =>
                {
                    b.HasOne("Domain.Models.Hrana", null)
                        .WithMany("Ocjene")
                        .HasForeignKey("HranaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.SavedBook", b =>
                {
                    b.HasOne("Domain.Models.Book", "Book")
                        .WithMany("BookUsers")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("BookUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
