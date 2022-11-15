﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmMarketScraper.Infrastructure.Persistence;

#nullable disable

namespace SmMarketScraper.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221115105936_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SmMarketScraper.Domain.Entities.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset?>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("FinalPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<decimal?>("MaxQuantity")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal?>("RegularPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("UoM")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SmMarketScraper.Domain.Entities.ProductHistory", b =>
                {
                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<string>("Property")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uuid");

                    b.Property<string>("NewValue")
                        .HasColumnType("text");

                    b.Property<string>("OldValue")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ProductId", "Property", "AggregateId");

                    b.ToTable("ProductHistories");
                });

            modelBuilder.Entity("SmMarketScraper.Domain.Entities.ProductHistory", b =>
                {
                    b.HasOne("SmMarketScraper.Domain.Entities.Product", "Product")
                        .WithMany("ProductHistories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SmMarketScraper.Domain.Entities.Product", b =>
                {
                    b.Navigation("ProductHistories");
                });
#pragma warning restore 612, 618
        }
    }
}