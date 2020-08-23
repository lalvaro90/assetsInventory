﻿// <auto-generated />
using System;
using AssetsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssetsApi.Migrations
{
    [DbContext(typeof(AssetsContext))]
    [Migration("20200818155307_update person entity")]
    partial class updatepersonentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssetsApi.Models.AcquisitionMethod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ID");

                    b.ToTable("AcquisitionMethods");
                });

            modelBuilder.Entity("AssetsApi.Models.Asset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AcquisitionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("AcquisitionMethodID")
                        .HasColumnType("int");

                    b.Property<string>("AssetId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Assiento")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CurrentPrice")
                        .HasColumnType("float");

                    b.Property<int?>("DepreciationId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Folio")
                        .HasColumnType("int");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LocationID")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ProviderID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("PurchasePrice")
                        .HasColumnType("float");

                    b.Property<int?>("Responsible2ID")
                        .HasColumnType("int");

                    b.Property<int?>("ResponsibleID")
                        .HasColumnType("int");

                    b.Property<string>("Series")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StateID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Tomo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("AssetId");

                    b.HasIndex("AcquisitionMethodID");

                    b.HasIndex("DepreciationId");

                    b.HasIndex("LocationID");

                    b.HasIndex("ProviderID");

                    b.HasIndex("Responsible2ID");

                    b.HasIndex("ResponsibleID");

                    b.HasIndex("StateID");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("AssetsApi.Models.AssetHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("AssetID")
                        .HasColumnType("bigint");

                    b.Property<string>("NewDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreviewsDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AssetsHistory");
                });

            modelBuilder.Entity("AssetsApi.Models.AssetNotes", b =>
                {
                    b.Property<long>("IdNote")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AssetId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdNote");

                    b.HasIndex("AssetId");

                    b.HasIndex("CreatedById");

                    b.ToTable("AssetNotes");
                });

            modelBuilder.Entity("AssetsApi.Models.Configuration", b =>
                {
                    b.Property<int>("IdConfig")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Assiento")
                        .HasColumnType("int");

                    b.Property<string>("Circuito")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Folio")
                        .HasColumnType("int");

                    b.Property<string>("IdPrefix")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstituteName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstitutionLogo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentLogo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Supervisor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tomo")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("IdConfig");

                    b.ToTable("Configuration");
                });

            modelBuilder.Entity("AssetsApi.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("AssetsApi.Models.Depreciation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Frequency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastRun")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NextRun")
                        .HasColumnType("datetime2");

                    b.Property<double>("Percentage")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Depreciations");
                });

            modelBuilder.Entity("AssetsApi.Models.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Responsible1ID")
                        .HasColumnType("int");

                    b.Property<int?>("Responsible2ID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Responsible1ID");

                    b.HasIndex("Responsible2ID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("AssetsApi.Models.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("AssetsApi.Models.Provider", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("AssetsApi.Models.States", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("States");
                });

            modelBuilder.Entity("AssetsApi.Models.Token", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expire")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Content");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("AssetsApi.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Permissions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AssetsApi.Models.Asset", b =>
                {
                    b.HasOne("AssetsApi.Models.AcquisitionMethod", "AcquisitionMethod")
                        .WithMany()
                        .HasForeignKey("AcquisitionMethodID");

                    b.HasOne("AssetsApi.Models.Depreciation", "Depreciation")
                        .WithMany()
                        .HasForeignKey("DepreciationId");

                    b.HasOne("AssetsApi.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID");

                    b.HasOne("AssetsApi.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderID");

                    b.HasOne("AssetsApi.Models.Person", "Responsible2")
                        .WithMany()
                        .HasForeignKey("Responsible2ID");

                    b.HasOne("AssetsApi.Models.Person", "Responsible")
                        .WithMany()
                        .HasForeignKey("ResponsibleID");

                    b.HasOne("AssetsApi.Models.States", "State")
                        .WithMany()
                        .HasForeignKey("StateID");
                });

            modelBuilder.Entity("AssetsApi.Models.AssetNotes", b =>
                {
                    b.HasOne("AssetsApi.Models.Asset", "Asset")
                        .WithMany()
                        .HasForeignKey("AssetId");

                    b.HasOne("AssetsApi.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });

            modelBuilder.Entity("AssetsApi.Models.Location", b =>
                {
                    b.HasOne("AssetsApi.Models.Person", "Responsible1")
                        .WithMany()
                        .HasForeignKey("Responsible1ID");

                    b.HasOne("AssetsApi.Models.Person", "Responsible2")
                        .WithMany()
                        .HasForeignKey("Responsible2ID");
                });
#pragma warning restore 612, 618
        }
    }
}
