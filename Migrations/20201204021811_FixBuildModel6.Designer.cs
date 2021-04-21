﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentRoom.Models;

namespace RentRoom.Migrations
{
    [DbContext(typeof(RentRoomContext))]
    [Migration("20201204021811_FixBuildModel6")]
    partial class FixBuildModel6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RentRoom.Models.Build", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bathroom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdd")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Elevator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FloorRent")
                        .HasColumnType("int");

                    b.Property<int>("Floors")
                        .HasColumnType("int");

                    b.Property<string>("Furniture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GarbageChute")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseholdAppliances")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Internet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsNumberShow")
                        .HasColumnType("bit");

                    b.Property<int>("KitchenArea")
                        .HasColumnType("int");

                    b.Property<int>("LivingArea")
                        .HasColumnType("int");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Photo2")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Photo3")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Repairs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rooms")
                        .HasColumnType("int");

                    b.Property<int>("RoomsToRent")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TotalArea")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("YearBuilt")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Builds");
                });

            modelBuilder.Entity("RentRoom.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Admin")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RentRoom.Models.Build", b =>
                {
                    b.HasOne("RentRoom.Models.User", "User")
                        .WithMany("Builds")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
