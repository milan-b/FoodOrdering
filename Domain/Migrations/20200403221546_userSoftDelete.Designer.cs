﻿// <auto-generated />
using System;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(HranaContext))]
    [Migration("20200403221546_userSoftDelete")]
    partial class userSoftDelete
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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

                    b.Property<int>("HranaPrilogId")
                        .HasColumnType("int");

                    b.Property<int>("Varijanta")
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

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Lozinka")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Prezime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TimeId")
                        .HasColumnType("int");

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

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("MeniId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TimeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NarudzbaId");

                    b.HasIndex("MeniId");

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("Domain.Models.OrderSideDish", b =>
                {
                    b.Property<int>("NarudzbaId")
                        .HasColumnType("int");

                    b.Property<int>("PrilogId")
                        .HasColumnType("int");

                    b.Property<int>("OrderSideDishId")
                        .HasColumnType("int");

                    b.HasKey("NarudzbaId", "PrilogId");

                    b.HasIndex("PrilogId");

                    b.ToTable("OrderSideDish");
                });

            modelBuilder.Entity("Domain.Models.Prilog", b =>
                {
                    b.Property<int>("PrilogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("PrilogId");

                    b.ToTable("Prilog");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<string>("Roles")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TimeId")
                        .HasColumnType("int");

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
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Models.Meni", "Meni")
                        .WithMany("Hrana")
                        .HasForeignKey("MeniId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.HranaPrilog", b =>
                {
                    b.HasOne("Domain.Models.Hrana", "Hrana")
                        .WithMany("Prilozi")
                        .HasForeignKey("HranaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Models.Prilog", "Prilog")
                        .WithMany("Hrana")
                        .HasForeignKey("PrilogId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Komentar", b =>
                {
                    b.HasOne("Domain.Models.Hrana", null)
                        .WithMany("Komentari")
                        .HasForeignKey("HranaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Narudzba", b =>
                {
                    b.HasOne("Domain.Models.Meni", null)
                        .WithMany("Narudzbe")
                        .HasForeignKey("MeniId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Ocjena", b =>
                {
                    b.HasOne("Domain.Models.Hrana", null)
                        .WithMany("Ocjene")
                        .HasForeignKey("HranaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.OrderSideDish", b =>
                {
                    b.HasOne("Domain.Models.Narudzba", "Narudzba")
                        .WithMany("SideDishes")
                        .HasForeignKey("NarudzbaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Models.Prilog", "Prilog")
                        .WithMany("Orders")
                        .HasForeignKey("PrilogId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
