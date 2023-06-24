﻿// <auto-generated />
using System;
using DDRC.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DDRC.WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230624042834_StocksTable")]
    partial class StocksTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ActorModelMovieModel", b =>
                {
                    b.Property<Guid>("ActorsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MoviesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ActorsId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("ActorModelMovieModel");
                });

            modelBuilder.Entity("CategoryModelMovieModel", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MoviesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriesId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("CategoryModelMovieModel");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.ActorModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ActorModel");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.CategoryModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryModel");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.ExpectedSaleModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VideoStoreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("VideoStoreId");

                    b.ToTable("ExpectedSaleModel");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.FulfilledSaleModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VideoStoreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("VideoStoreId");

                    b.ToTable("FulfilledSaleModel");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.MovieModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovieModel");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.StockModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("StockModel");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.VideoStoreModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VideoStoreModel");
                });

            modelBuilder.Entity("ActorModelMovieModel", b =>
                {
                    b.HasOne("DDRC.WebApi.Models.ActorModel", null)
                        .WithMany()
                        .HasForeignKey("ActorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DDRC.WebApi.Models.MovieModel", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CategoryModelMovieModel", b =>
                {
                    b.HasOne("DDRC.WebApi.Models.CategoryModel", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DDRC.WebApi.Models.MovieModel", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DDRC.WebApi.Models.ExpectedSaleModel", b =>
                {
                    b.HasOne("DDRC.WebApi.Models.MovieModel", "Movie")
                        .WithMany("ExpectedSales")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DDRC.WebApi.Models.VideoStoreModel", "VideoStore")
                        .WithMany("ExpectedSales")
                        .HasForeignKey("VideoStoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("VideoStore");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.FulfilledSaleModel", b =>
                {
                    b.HasOne("DDRC.WebApi.Models.MovieModel", "Movie")
                        .WithMany("FulfilledSales")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DDRC.WebApi.Models.VideoStoreModel", "VideoStore")
                        .WithMany("FulfilledSales")
                        .HasForeignKey("VideoStoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("VideoStore");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.StockModel", b =>
                {
                    b.HasOne("DDRC.WebApi.Models.MovieModel", "Movie")
                        .WithMany("Stocks")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.MovieModel", b =>
                {
                    b.Navigation("ExpectedSales");

                    b.Navigation("FulfilledSales");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("DDRC.WebApi.Models.VideoStoreModel", b =>
                {
                    b.Navigation("ExpectedSales");

                    b.Navigation("FulfilledSales");
                });
#pragma warning restore 612, 618
        }
    }
}
