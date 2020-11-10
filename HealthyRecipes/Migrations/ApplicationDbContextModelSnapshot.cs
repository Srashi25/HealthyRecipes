﻿// <auto-generated />
using System;
using HealthyRecipes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HealthyRecipes.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HealthyRecipes.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .IsRequired();

                    b.Property<int>("CookingTime");

                    b.Property<DateTime>("Date");

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<string>("Ingredient1")
                        .IsRequired();

                    b.Property<string>("Ingredient2")
                        .IsRequired();

                    b.Property<string>("Ingredient3")
                        .IsRequired();

                    b.Property<string>("Ingredient4")
                        .IsRequired();

                    b.Property<string>("Ingredient5");

                    b.Property<string>("Ingredient6");

                    b.Property<string>("Instructions")
                        .IsRequired();

                    b.Property<DateTime?>("LastEdit");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("RecipeAvgRating");

                    b.Property<string>("UserId");

                    b.HasKey("RecipeID");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("HealthyRecipes.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RecipeID");

                    b.Property<int>("RecipeRating");

                    b.Property<string>("ReviewDescription")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("ReviewID");

                    b.HasIndex("RecipeID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("HealthyRecipes.Models.Review", b =>
                {
                    b.HasOne("HealthyRecipes.Models.Recipe", "Recipe")
                        .WithMany("Reviews")
                        .HasForeignKey("RecipeID");
                });
#pragma warning restore 612, 618
        }
    }
}
