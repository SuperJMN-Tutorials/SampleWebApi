﻿// <auto-generated />
using System;
using ApiEjemplo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiEjemplo.Migrations
{
    [DbContext(typeof(BikingContext))]
    partial class StravaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiEjemplo.Features.Activities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Creator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("FinishDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("ApiEjemplo.Model.Bike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Bikes");
                });

            modelBuilder.Entity("ApiEjemplo.Model.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("AddedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("BikeId")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ComponentType")
                        .HasColumnType("int");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("RetiredOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BikeId");

                    b.ToTable("Component");
                });

            modelBuilder.Entity("ApiEjemplo.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ApiEjemplo.Model.Bike", b =>
                {
                    b.HasOne("ApiEjemplo.Model.User", null)
                        .WithMany("Bikes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ApiEjemplo.Model.Component", b =>
                {
                    b.HasOne("ApiEjemplo.Model.Bike", null)
                        .WithMany("Components")
                        .HasForeignKey("BikeId");
                });

            modelBuilder.Entity("ApiEjemplo.Model.Bike", b =>
                {
                    b.Navigation("Components");
                });

            modelBuilder.Entity("ApiEjemplo.Model.User", b =>
                {
                    b.Navigation("Bikes");
                });
#pragma warning restore 612, 618
        }
    }
}
