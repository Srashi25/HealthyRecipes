using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyRecipes.Models
{
    public class EFRecipeRepository: IRecipeRepository
    {
        private ApplicationDbContext context;

        public EFRecipeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Recipe> Recipes => context.Recipes
                                    .Include(r => r.Reviews);


        public void SaveRecipe(Recipe recipe)
        {
            if (recipe.RecipeID == 0)
            {
                context.Recipes.Add(recipe);
            }
            else
            {
                Recipe dbEntry = context.Recipes
                    .FirstOrDefault(r => r.RecipeID == recipe.RecipeID);
                if (dbEntry != null)
                {
                    dbEntry.Name = recipe.Name;
                    dbEntry.UserId = recipe.UserId;
                    dbEntry.LastEdit = recipe.LastEdit;
                    dbEntry.ImageUrl = recipe.ImageUrl;
                    dbEntry.Ingredient1 = recipe.Ingredient1;
                    dbEntry.Ingredient2 = recipe.Ingredient2;
                    dbEntry.Ingredient3 = recipe.Ingredient3;
                    dbEntry.Ingredient4 = recipe.Ingredient4;
                    dbEntry.Ingredient5 = recipe.Ingredient5;
                    dbEntry.Ingredient6 = recipe.Ingredient6;
                    dbEntry.Instructions = recipe.Instructions;
                    dbEntry.CookingTime = recipe.CookingTime;
                    dbEntry.Category = recipe.Category;
                    dbEntry.Date = recipe.Date;
                    dbEntry.Reviews = recipe.Reviews;

                }
            }
            context.SaveChanges();
        }

        public Recipe DeleteRecipe(int recipeID)
        {

            Recipe dbEntry = context.Recipes
                .FirstOrDefault(r => r.RecipeID == recipeID);
            if (dbEntry != null)
            {
                context.Recipes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
        public void EditReview(Review review)
        {
                Review _dbEntry = context.Reviews
                    .FirstOrDefault(r => r.ReviewID == review.ReviewID);
                if (_dbEntry != null)
                {
                    _dbEntry.Recipe = review.Recipe;
                    _dbEntry.ReviewID = review.ReviewID;
                    _dbEntry.ReviewDescription = review.ReviewDescription;
                    _dbEntry.Title = review.Title;
                     _dbEntry.UserId = review.UserId;
                }
           
            context.SaveChanges();
        }
        public void DelReview(Review review)
        {

            Review dbEntry = context.Reviews
                .FirstOrDefault(r => r.ReviewID == review.ReviewID);
            if (dbEntry != null)
            {
                context.Reviews.Remove(dbEntry);
                context.SaveChanges();
            }
        }

    }

}

