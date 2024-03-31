﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Zip.WebAPI.Data;

#nullable disable

namespace Zip.WebAPI.Migrations
{
    [DbContext(typeof(ZipPayContext))]
    [Migration("20240331221841_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Zip.WebAPI.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.Property<int>("Userid")
                        .HasColumnType("integer")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Userid" }, "accounts_userid_unique")
                        .IsUnique();

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("Zip.WebAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<decimal?>("Expenses")
                        .HasPrecision(8, 2)
                        .HasColumnType("numeric(8,2)")
                        .HasColumnName("expenses");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<decimal?>("Salary")
                        .HasPrecision(8, 2)
                        .HasColumnType("numeric(8,2)")
                        .HasColumnName("salary");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "users_email_key")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Zip.WebAPI.Models.Account", b =>
                {
                    b.HasOne("Zip.WebAPI.Models.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("Zip.WebAPI.Models.Account", "Userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("accounts_users_fk");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Zip.WebAPI.Models.User", b =>
                {
                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
