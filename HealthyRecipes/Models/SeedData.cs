using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyRecipes.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
            .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Recipes.Any())
            {
                context.Recipes.AddRange(
            new Recipe { Name = "Chicken Rice",Date=DateTime.Now ,UserId="Admin",ImageUrl = "https://api.norecipes.com/wp-content/uploads/2018/08/teriyaki-chicken-recipe_007.jpg", CookingTime = 10, Category = "Chicken", Ingredient1 = "Flour", Ingredient2 = "Milk", Ingredient3 = "Eggs", Ingredient4 = "Sugar", Ingredient5 = "Oil", Ingredient6 = "Water", Instructions = "    Bring 3 cups water and 1/2 teaspoon salt to a boil.Add 1 cup cereal and turn heat to low. Cover and cook for about 10 minutes, stirring occasionally.Makes enough for four hungry folks (about 2-1/2 cups). Serve with honey (or brown sugar) and milk." },
            new Recipe { Name = "Chicken Pie", Date = DateTime.Now, UserId = "Admin1" ,ImageUrl = "https://www.cbc.ca/food/content/images/recipes/WinterVegPie.jpg", Category = "Chicken", CookingTime = 15, Ingredient1 = "Flour", Ingredient2 = "Sugar", Ingredient3 = "Chocolate Podwer", Ingredient4 = "Butter", Ingredient5 = "Eggs", Ingredient6 = "Milk", Instructions = "Cream butter and sugar until fluffy. Beat in egg and vanila. Add baking porder and salt. Beat well. Add flour and mix until blended. Remove 2/3 of dough. Roll or pat dough out to the form og 10 x 4 inch rectangles." },
            new Recipe { Name = "Chicken Cookie", Date = DateTime.Now, UserId = "Admin", ImageUrl = "https://www.inspiredtaste.net/wp-content/uploads/2013/01/Roasted-Pepper-Pinwheel-Pastry-Recipe-2-1200.jpg", CookingTime = 8, Category = "Chicken" ,Ingredient1 = "Flour", Ingredient2 = "Sugar", Ingredient3 = "Chocolate Podwer", Ingredient4 = "Butter", Ingredient5 = "Eggs", Ingredient6 = "Cream Cheese", Instructions = "Cream butter and sugar until fluffy. Beat in egg and vanila. Add baking porder and salt. Beat well. Add flour and mix until blended. Remove 2/3 of dough. Roll or pat dough out to the form og 10 x 4 inch rectangles." },
            new Recipe { Name = "Cookie", Date = DateTime.Now, UserId = "Admin1", ImageUrl = "https://assets.kraftfoods.com/recipe_images/opendeploy/535545_2_1_retail-c4addebe7ca68292fbfd57c00408296ae813afc6_642x428.jpg", CookingTime = 30, Category = "Cookies", Ingredient1 = "Flour", Ingredient2 = "1 cup butter, softened", Ingredient3 = "Chocolate Podwer", Ingredient4 = "2 tsp. vanilla", Ingredient5 = "1 tsp. baking soda", Ingredient6 = "1 pkg. (300 g) Baker's Semi-Sweet Chocolate Chips", Instructions = "Heat oven to 375°F.Beat butter,sugars,eggs and vanilla in large bowl until light and fluffy.Stir in flour and baking soda until well blended.Add chocolate chips; mix well.Drop tablespoonfuls of dough, 2 inches apart, onto baking sheets.Bake 10 to 12 min.or until lightly browned. Cool on baking sheet 3 min.Remove to wire racks; cool completely. " },
             new Recipe { Name = "Vanila Cake", Date = DateTime.Now, UserId = "Admin",ImageUrl = "https://sugarspunrun.com/wp-content/uploads/2018/01/Vanilla-Cake-Recipe-1-of-1-15.jpg", CookingTime = 8, Category = "Cake", Ingredient1 = "Flour", Ingredient2 = "1 cup butter, softened", Ingredient3 = "Chocolate Podwer", Ingredient4 = "2 tsp. vanilla", Ingredient5 = "1 tsp. baking soda", Ingredient6 = "1 pkg. (300 g) Baker's Semi-Sweet Chocolate Chips", Instructions = "Heat oven to 375°F.Beat butter,sugars,eggs and vanilla in large bowl until light and fluffy.Stir in flour and baking soda until well blended.Add chocolate chips; mix well.Drop tablespoonfuls of dough, 2 inches apart, onto baking sheets.Bake 10 to 12 min.or until lightly browned. Cool on baking sheet 3 min.Remove to wire racks; cool completely. " });
                context.SaveChanges();
            }
        }
    }
}
