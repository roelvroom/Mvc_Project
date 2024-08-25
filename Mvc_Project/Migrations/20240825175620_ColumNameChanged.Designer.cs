﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mvc_Project.Data;

#nullable disable

namespace Mvc_Project.Migrations
{
    [DbContext(typeof(Mvc_ProjectContext))]
    [Migration("20240825175620_ColumNameChanged")]
    partial class ColumNameChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mvc_Project.Models.Aankoop", b =>
                {
                    b.Property<int>("AankoopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AankoopId"), 1L, 1);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<bool>("GoedGekeurd")
                        .HasColumnType("bit");

                    b.Property<int>("IDProduct")
                        .HasColumnType("int");

                    b.Property<int>("NaamAanvragerId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Reden")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotaalPrijs")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VakId")
                        .HasColumnType("int");

                    b.Property<bool>("Verwijderd")
                        .HasColumnType("bit");

                    b.HasKey("AankoopId");

                    b.HasIndex("NaamAanvragerId");

                    b.HasIndex("ProductId");

                    b.ToTable("Aankoop", (string)null);
                });

            modelBuilder.Entity("Mvc_Project.Models.Bijlagen", b =>
                {
                    b.Property<int>("BijlagenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BijlagenId"), 1L, 1);

                    b.Property<int>("AankoopId")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BijlagenId");

                    b.ToTable("Bijlagen", (string)null);
                });

            modelBuilder.Entity("Mvc_Project.Models.Gebruiker", b =>
                {
                    b.Property<int>("GebruikerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GebruikerId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GebruikersNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Initialen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Verwijder")
                        .HasColumnType("bit");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GebruikerId");

                    b.ToTable("Gebruiker", (string)null);
                });

            modelBuilder.Entity("Mvc_Project.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<string>("Hoevelheid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Prijs")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Producten", (string)null);
                });

            modelBuilder.Entity("Mvc_Project.Models.Vak", b =>
                {
                    b.Property<int>("VakId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VakId"), 1L, 1);

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Verwijderd")
                        .HasColumnType("bit");

                    b.HasKey("VakId");

                    b.ToTable("Vak", (string)null);
                });

            modelBuilder.Entity("Mvc_Project.Models.Aankoop", b =>
                {
                    b.HasOne("Mvc_Project.Models.Gebruiker", "NaamAanvrager")
                        .WithMany()
                        .HasForeignKey("NaamAanvragerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mvc_Project.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NaamAanvrager");

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
