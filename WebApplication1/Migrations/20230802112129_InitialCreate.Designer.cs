﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication1.Database;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(WarehouseContext))]
    [Migration("20230802112129_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Database.Entities.Item", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("Warehouseid")
                        .HasColumnType("uuid");

                    b.Property<string>("description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("tenantId")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.HasIndex("Warehouseid");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("WebApplication1.Database.Entities.Location", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("latitude")
                        .HasColumnType("text");

                    b.Property<string>("longitude")
                        .HasColumnType("text");

                    b.Property<string>("postalCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("WebApplication1.Database.Entities.Warehouse", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("dateClosedUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("dateOpenUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("dateTimeCreatedUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("defaultLanguage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("deleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("isPayoutLockedForOtherCostCenter")
                        .HasColumnType("boolean");

                    b.Property<Guid>("locationid")
                        .HasColumnType("uuid");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("tenantId")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.HasIndex("locationid");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("WebApplication1.Database.Entities.Item", b =>
                {
                    b.HasOne("WebApplication1.Database.Entities.Warehouse", null)
                        .WithMany("items")
                        .HasForeignKey("Warehouseid");
                });

            modelBuilder.Entity("WebApplication1.Database.Entities.Warehouse", b =>
                {
                    b.HasOne("WebApplication1.Database.Entities.Location", "location")
                        .WithMany()
                        .HasForeignKey("locationid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("location");
                });

            modelBuilder.Entity("WebApplication1.Database.Entities.Warehouse", b =>
                {
                    b.Navigation("items");
                });
#pragma warning restore 612, 618
        }
    }
}
