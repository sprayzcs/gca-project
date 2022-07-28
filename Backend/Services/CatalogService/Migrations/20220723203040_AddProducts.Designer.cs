﻿// <auto-generated />
using System;
using CatalogService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CatalogService.Migrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20220723203040_AddProducts")]
    partial class AddProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CatalogService.Data.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7cf05588-d2a7-4760-98b1-c464335711a7"),
                            Name = "Ibu 400",
                            Price = 50
                        },
                        new
                        {
                            Id = new Guid("ae66081f-d5c4-468d-96ef-52dc163cea32"),
                            Name = "Ibu 600",
                            Price = 75
                        },
                        new
                        {
                            Id = new Guid("669e6e98-752c-4309-98b6-176aa3c2c8cd"),
                            Name = "Ibu 800",
                            Price = 110
                        },
                        new
                        {
                            Id = new Guid("c0383405-68c4-4efb-b5c2-19e970a7b5e0"),
                            Name = "Grippostad C",
                            Price = 990
                        },
                        new
                        {
                            Id = new Guid("615917d5-b26f-4507-ac2b-554af8d888d0"),
                            Name = "Elotrans",
                            Price = 2299
                        },
                        new
                        {
                            Id = new Guid("dfeff251-ec28-4530-8f81-27260ec7ce34"),
                            Name = "Pantoprazol",
                            Price = 1099
                        },
                        new
                        {
                            Id = new Guid("b11de4ae-e4c9-464c-a494-2a4a5e866a38"),
                            Name = "Calciumfolinat",
                            Price = 53445
                        },
                        new
                        {
                            Id = new Guid("01938276-763b-4104-8763-dfd97654e10a"),
                            Name = "Marcumar",
                            Price = 2539
                        },
                        new
                        {
                            Id = new Guid("063c055d-2284-41d1-82d0-8c3ad77d7546"),
                            Name = "Tamoxifen",
                            Price = 10954
                        },
                        new
                        {
                            Id = new Guid("320cffdb-579f-4e05-934e-699e30d68fd2"),
                            Name = "Vitamin D3",
                            Price = 1239
                        });
                });
#pragma warning restore 612, 618
        }
    }
}