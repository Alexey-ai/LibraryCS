﻿// <auto-generated />
using System;
using LibraryCS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryCS.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20200127150440_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LibraryCS.Models.Book", b =>
                {
                    b.Property<int>("BookID");

                    b.Property<string>("Author");

                    b.Property<bool>("Aviability");

                    b.Property<DateTime>("BookAddDate");

                    b.Property<string>("Description");

                    b.Property<string>("Edition");

                    b.Property<string>("Genre");

                    b.Property<string>("NameBook");

                    b.HasKey("BookID");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("LibraryCS.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookID");

                    b.Property<DateTime>("OrderDate");

                    b.Property<DateTime?>("OrderReturnDate");

                    b.Property<int>("ReaderID");

                    b.HasKey("OrderID");

                    b.HasIndex("BookID")
                        .IsUnique();

                    b.HasIndex("ReaderID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("LibraryCS.Models.Reader", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Adress");

                    b.Property<int>("Age");

                    b.Property<string>("Passport");

                    b.Property<string>("Phone");

                    b.Property<string>("ReaderLastName");

                    b.Property<string>("ReaderName");

                    b.HasKey("ID");

                    b.ToTable("Reader");
                });

            modelBuilder.Entity("LibraryCS.Models.Order", b =>
                {
                    b.HasOne("LibraryCS.Models.Book", "Book")
                        .WithOne("Order")
                        .HasForeignKey("LibraryCS.Models.Order", "BookID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryCS.Models.Reader", "Reader")
                        .WithMany("Orders")
                        .HasForeignKey("ReaderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}