﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Organizations.Infrastructure;

#nullable disable

namespace Organizations.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231221152444_NullableOrganizationDescription")]
    partial class NullableOrganizationDescription
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Organizations.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<int>("UserType")
                        .HasColumnType("integer")
                        .HasColumnName("user_type");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Organizations.Domain.Entities.Organization", b =>
                {
                    b.HasBaseType("Organizations.Domain.Entities.User");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("inn");

                    b.Property<string>("Ogrn")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ogrn");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Organizations.Domain.Entities.Organization", b =>
                {
                    b.HasOne("Organizations.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Organizations.Domain.Entities.Organization", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organizations_users_id");
                });
#pragma warning restore 612, 618
        }
    }
}
