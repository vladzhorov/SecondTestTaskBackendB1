﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SecondTestTaskB1.Db;

#nullable disable

namespace SecondTestTaskB1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241118205736_ChangeDateTimeZone")]
    partial class ChangeDateTimeZone
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SecondTestTaskB1.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountClass")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("ClosingActive")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ClosingPassive")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Credit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Debit")
                        .HasColumnType("numeric");

                    b.Property<int>("FileId")
                        .HasColumnType("integer");

                    b.Property<decimal>("OpeningActive")
                        .HasColumnType("numeric");

                    b.Property<decimal>("OpeningPassive")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SecondTestTaskB1.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTimeOffset>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FileName")
                        .IsUnique();

                    b.ToTable("Files");
                });

            modelBuilder.Entity("SecondTestTaskB1.Models.Account", b =>
                {
                    b.HasOne("SecondTestTaskB1.Models.File", "File")
                        .WithMany("Accounts")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("SecondTestTaskB1.Models.File", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
