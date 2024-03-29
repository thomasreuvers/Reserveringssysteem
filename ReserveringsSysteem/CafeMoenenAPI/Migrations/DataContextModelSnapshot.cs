﻿// <auto-generated />
using System;
using CafeMoenenAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CafeMoenenAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CafeMoenenAPI.Models.ReservationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BookingDateTime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("BookingEndDateTime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("BookingName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Setting")
                        .HasColumnType("int");

                    b.Property<string>("UserCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("CafeMoenenAPI.Models.TableModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsOccupied")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ReservationModelId")
                        .HasColumnType("int");

                    b.Property<int>("Setting")
                        .HasColumnType("int");

                    b.Property<int>("TableNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReservationModelId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("CafeMoenenAPI.Models.UserModel", b =>
                {
                    b.Property<string>("UserCode")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Role")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserCode");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CafeMoenenAPI.Models.TableModel", b =>
                {
                    b.HasOne("CafeMoenenAPI.Models.ReservationModel", null)
                        .WithMany("Tables")
                        .HasForeignKey("ReservationModelId");
                });
#pragma warning restore 612, 618
        }
    }
}
