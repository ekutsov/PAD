﻿// <auto-generated />
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
    [Migration("20220907113151_Initial")]
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
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<string>("CategoryId")
                        .HasColumnType("text");

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
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExpsenseCategories");
                });

            modelBuilder.Entity("PAD.Finance.Infrastructure.Models.Expense", b =>
                {
                    b.HasOne("PAD.Finance.Infrastructure.Models.ExpenseCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}