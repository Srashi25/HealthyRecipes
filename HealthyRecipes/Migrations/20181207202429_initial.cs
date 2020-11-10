using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthyRecipes.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RecipeAvgRating = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Ingredient1 = table.Column<string>(nullable: false),
                    Ingredient2 = table.Column<string>(nullable: false),
                    Ingredient3 = table.Column<string>(nullable: false),
                    Ingredient4 = table.Column<string>(nullable: false),
                    Ingredient5 = table.Column<string>(nullable: true),
                    Ingredient6 = table.Column<string>(nullable: true),
                    Instructions = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    CookingTime = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    LastEdit = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeID);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    ReviewDescription = table.Column<string>(nullable: false),
                    RecipeID = table.Column<int>(nullable: true),
                    RecipeRating = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RecipeID",
                table: "Reviews",
                column: "RecipeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
