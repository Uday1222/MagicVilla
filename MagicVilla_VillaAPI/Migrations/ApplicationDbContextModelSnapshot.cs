﻿// <auto-generated />
using System;
using MagicVilla_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_VillaAPI.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 4, 24, 13, 42, 51, 743, DateTimeKind.Local).AddTicks(1977),
                            Details = "Royal Villa Details",
                            ImageUrl = "https://www.google.com/imgres?imgurl=https%3A%2F%2Fik.imagekit.io%2F5tgxhsqev%2Fbackup-bucket%2Ftr%3Aw-800%2Ch-460%2Cq-62%2Fimage%2Fupload%2Fr2jetjmjhj5i6yijouhp.webp&tbnid=mawYSi6wALGu5M&vet=12ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg..i&imgrefurl=https%3A%2F%2Fwww.saffronstays.com%2Fvillas%2Fvillas-in-Nagaon%2520Beach%3Flat%3D18.563400%26lon%3D72.871400%26nearby%3Dtrue&docid=WBtW53B88S-OrM&w=800&h=460&q=villas&ved=2ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg",
                            Name = "Royal Villa",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 500,
                            UpdateDate = new DateTime(2023, 4, 24, 13, 42, 51, 743, DateTimeKind.Local).AddTicks(2008)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 4, 24, 13, 42, 51, 743, DateTimeKind.Local).AddTicks(2019),
                            Details = "Royal Villa Details2",
                            ImageUrl = "https://www.google.com/imgres?imgurl=https%3A%2F%2Fik.imagekit.io%2F5tgxhsqev%2Fbackup-bucket%2Ftr%3Aw-800%2Ch-460%2Cq-62%2Fimage%2Fupload%2Fr2jetjmjhj5i6yijouhp.webp&tbnid=mawYSi6wALGu5M&vet=12ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg..i&imgrefurl=https%3A%2F%2Fwww.saffronstays.com%2Fvillas%2Fvillas-in-Nagaon%2520Beach%3Flat%3D18.563400%26lon%3D72.871400%26nearby%3Dtrue&docid=WBtW53B88S-OrM&w=800&h=460&q=villas&ved=2ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg",
                            Name = "Royal Villa2",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 500,
                            UpdateDate = new DateTime(2023, 4, 24, 13, 42, 51, 743, DateTimeKind.Local).AddTicks(2021)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
