﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PAD.Finance.Infrastructure.Data;

#nullable disable

namespace PAD.Finance.Infrastructure.Migrations
{
    [DbContext(typeof(FinanceDbContext))]
    [Migration("20220908122803_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PAD.Finance.Infrastructure.Models.Expense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsExcluded")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Expsenses");
                });

            modelBuilder.Entity("PAD.Finance.Infrastructure.Models.ExpenseCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExpsenseCategories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7b48e3bc-a2e5-4e97-a14f-5baccc826b6f"),
                            Name = "Foodstuff"
                        },
                        new
                        {
                            Id = new Guid("1e61b920-12aa-4093-b009-9ec9194b9bfa"),
                            Name = "Utility bills"
                        });
                });

            modelBuilder.Entity("PAD.Finance.Infrastructure.Models.Expense", b =>
                {
                    b.HasOne("PAD.Finance.Infrastructure.Models.ExpenseCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}