﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using restaurant_api.Infrastructure.Database;

#nullable disable

namespace restaurantapi.Migrations
{
    [DbContext(typeof(RestaurantDbContext))]
    partial class RestaurantDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("restaurant_api.Models.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Burgers")
                        .HasColumnType("integer");

                    b.Property<bool>("BurgersReady")
                        .HasColumnType("boolean");

                    b.Property<bool>("Delivered")
                        .HasColumnType("boolean");

                    b.Property<int>("Fries")
                        .HasColumnType("integer");

                    b.Property<bool>("FriesReady")
                        .HasColumnType("boolean");

                    b.Property<int>("Sodas")
                        .HasColumnType("integer");

                    b.Property<bool>("SodasReady")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
