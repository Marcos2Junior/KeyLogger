﻿// <auto-generated />
using System;
using KeyLoggerAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KeyLoggerAPI.Migrations
{
    [DbContext(typeof(KeyLoggerContext))]
    [Migration("20210317123743_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("KeyLoggerAPI.Entitys.Log", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Origin")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("KeyLoggerAPI.Entitys.RegisterLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("LogID")
                        .HasColumnType("int");

                    b.Property<string>("Register")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.HasIndex("LogID");

                    b.ToTable("RegisterLogs");
                });

            modelBuilder.Entity("KeyLoggerAPI.Entitys.RegisterLog", b =>
                {
                    b.HasOne("KeyLoggerAPI.Entitys.Log", "Log")
                        .WithMany("RegisterLogs")
                        .HasForeignKey("LogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Log");
                });

            modelBuilder.Entity("KeyLoggerAPI.Entitys.Log", b =>
                {
                    b.Navigation("RegisterLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
