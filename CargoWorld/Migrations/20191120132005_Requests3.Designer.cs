﻿// <auto-generated />
using System;
using CargoWorld.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CargoWorld.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191120132005_Requests3")]
    partial class Requests3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CargoWorld.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Geoposition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CargoWorld.Models.Car", b =>
                {
                    b.Property<int>("IdCar")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarcassNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CarryingCapacity")
                        .HasColumnType("int");

                    b.Property<double>("CarryingCapacitySqM")
                        .HasColumnType("float");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CostPerKm")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("HeightCargoCompartment")
                        .HasColumnType("float");

                    b.Property<string>("IdDriver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdGroup1")
                        .HasColumnType("int");

                    b.Property<string>("IdOwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("LengthCargoCompartment")
                        .HasColumnType("float");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WidthCargoCompartment")
                        .HasColumnType("float");

                    b.HasKey("IdCar");

                    b.HasIndex("IdGroup1");

                    b.HasIndex("IdOwnerId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CargoWorld.Models.Cargo", b =>
                {
                    b.Property<int>("Id_Cargo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CargoName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CargoType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeparturePoint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationPoint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<string>("Id_OwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDelivered")
                        .HasColumnType("bit");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TransferId_Delivery")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id_Cargo");

                    b.HasIndex("Id_OwnerId");

                    b.HasIndex("TransferId_Delivery");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("CargoWorld.Models.CargoInCar", b =>
                {
                    b.Property<int>("Id_Delivery")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmountOfCarog")
                        .HasColumnType("int");

                    b.Property<int?>("Id_CarIdCar")
                        .HasColumnType("int");

                    b.HasKey("Id_Delivery");

                    b.HasIndex("Id_CarIdCar");

                    b.ToTable("CargoInCars");
                });

            modelBuilder.Entity("CargoWorld.Models.Group", b =>
                {
                    b.Property<int>("IdGroup")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdOwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IdGroup");

                    b.HasIndex("IdOwnerId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("CargoWorld.Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdCar")
                        .HasColumnType("int");

                    b.Property<int?>("IdCargo")
                        .HasColumnType("int");

                    b.Property<int?>("IdGroup")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecipientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RequestType")
                        .HasColumnType("int");

                    b.Property<string>("SenderId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CargoWorld.Models.Car", b =>
                {
                    b.HasOne("CargoWorld.Models.Group", "IdGroup")
                        .WithMany()
                        .HasForeignKey("IdGroup1");

                    b.HasOne("CargoWorld.Models.ApplicationUser", "IdOwner")
                        .WithMany("Cars")
                        .HasForeignKey("IdOwnerId");
                });

            modelBuilder.Entity("CargoWorld.Models.Cargo", b =>
                {
                    b.HasOne("CargoWorld.Models.ApplicationUser", "Id_Owner")
                        .WithMany("Cargos")
                        .HasForeignKey("Id_OwnerId");

                    b.HasOne("CargoWorld.Models.CargoInCar", "Transfer")
                        .WithMany("Id_Cargo")
                        .HasForeignKey("TransferId_Delivery");
                });

            modelBuilder.Entity("CargoWorld.Models.CargoInCar", b =>
                {
                    b.HasOne("CargoWorld.Models.Car", "Id_Car")
                        .WithMany("CargoInThisCar")
                        .HasForeignKey("Id_CarIdCar");
                });

            modelBuilder.Entity("CargoWorld.Models.Group", b =>
                {
                    b.HasOne("CargoWorld.Models.ApplicationUser", "IdOwner")
                        .WithMany("Groups")
                        .HasForeignKey("IdOwnerId");
                });

            modelBuilder.Entity("CargoWorld.Models.Request", b =>
                {
                    b.HasOne("CargoWorld.Models.ApplicationUser", "Recipient")
                        .WithMany("RequestsToMe")
                        .HasForeignKey("RecipientId");
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
                    b.HasOne("CargoWorld.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CargoWorld.Models.ApplicationUser", null)
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

                    b.HasOne("CargoWorld.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CargoWorld.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
