﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using chess.games.db.Entities;

namespace chess.games.db.Migrations
{
    [DbContext(typeof(ChessGamesDbContext))]
    [Migration("20190817114638_create-GameImports-table")]
    partial class createGameImportstable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("chess.games.db.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("chess.games.db.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BlackId");

                    b.Property<string>("Date")
                        .HasMaxLength(30);

                    b.Property<Guid?>("EventId");

                    b.Property<string>("MoveText");

                    b.Property<int>("Result");

                    b.Property<string>("Round");

                    b.Property<Guid?>("SiteId");

                    b.Property<Guid?>("WhiteId");

                    b.HasKey("Id");

                    b.HasIndex("BlackId");

                    b.HasIndex("EventId");

                    b.HasIndex("SiteId");

                    b.HasIndex("WhiteId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("chess.games.db.Entities.GameImport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BlackId");

                    b.Property<string>("Date")
                        .HasMaxLength(30);

                    b.Property<Guid?>("EventId");

                    b.Property<string>("MoveText");

                    b.Property<int>("Result");

                    b.Property<string>("Round");

                    b.Property<Guid?>("SiteId");

                    b.Property<Guid?>("WhiteId");

                    b.HasKey("Id");

                    b.HasIndex("BlackId");

                    b.HasIndex("EventId");

                    b.HasIndex("SiteId");

                    b.HasIndex("WhiteId");

                    b.ToTable("GameImports");
                });

            modelBuilder.Entity("chess.games.db.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("chess.games.db.Entities.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("chess.games.db.Entities.Game", b =>
                {
                    b.HasOne("chess.games.db.Entities.Player", "Black")
                        .WithMany()
                        .HasForeignKey("BlackId");

                    b.HasOne("chess.games.db.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("chess.games.db.Entities.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId");

                    b.HasOne("chess.games.db.Entities.Player", "White")
                        .WithMany()
                        .HasForeignKey("WhiteId");
                });

            modelBuilder.Entity("chess.games.db.Entities.GameImport", b =>
                {
                    b.HasOne("chess.games.db.Entities.Player", "Black")
                        .WithMany()
                        .HasForeignKey("BlackId");

                    b.HasOne("chess.games.db.Entities.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("chess.games.db.Entities.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId");

                    b.HasOne("chess.games.db.Entities.Player", "White")
                        .WithMany()
                        .HasForeignKey("WhiteId");
                });
#pragma warning restore 612, 618
        }
    }
}
