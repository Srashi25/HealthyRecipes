using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyRecipes.Models
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> Recipes { get; }

        void SaveRecipe(Recipe recipe);
        Recipe DeleteRecipe(int recipeID);
        void EditReview(Review review);
        void DelReview(Review review);
    }
}
