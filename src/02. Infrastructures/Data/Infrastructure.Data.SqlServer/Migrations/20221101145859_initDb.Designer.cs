﻿// <auto-generated />
using System;
using Infrastructure.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.SqlServer.Migrations
{
    [DbContext(typeof(ShortUrlContext))]
    [Migration("20221101145859_initDb")]
    partial class initDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Domain.ShortUrl.Entities.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReviewDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ShortUrlId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShortUrlId");

                    b.ToTable("reviews");
                });

            modelBuilder.Entity("Core.Domain.ShortUrl.Entities.ShortUrl", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ShortUrlString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("UrlString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShortUrls");
                });

            modelBuilder.Entity("Core.Domain.ShortUrl.Entities.Review", b =>
                {
                    b.HasOne("Core.Domain.ShortUrl.Entities.ShortUrl", null)
                        .WithMany("Reviews")
                        .HasForeignKey("ShortUrlId");
                });

            modelBuilder.Entity("Core.Domain.ShortUrl.Entities.ShortUrl", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
