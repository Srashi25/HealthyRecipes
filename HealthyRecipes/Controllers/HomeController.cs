using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyRecipes.Models;
using HealthyRecipes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthyRecipes.Controllers
{
    public class HomeController : Controller
    {
        private IRecipeRepository repository;
        private int PageSize = 3;
        public HomeController(IRecipeRepository repo)
        {
            repository = repo;
        }
       
        public ViewResult Index() => View();
     
        public IActionResult RecipesList(string category, int recipePage = 1)
        => View(new RecipesListViewModel
        {
        Recipes = repository.Recipes
        .Where(r => category == null || r.Category == category)
        .OrderBy(r => r.RecipeID)
        .Skip((recipePage - 1) * PageSize)
        .Take(PageSize),
        PagingInfo = new PagingInfo
            {
        CurrentPage = recipePage,
        ItemsPerPage = PageSize,
            TotalItems = category == null ?
            repository.Recipes.Count() :
            repository.Recipes.Where(r =>
            r.Category == category).Count()
        },
            CurrentCategory = category
        });
       
        public ViewResult ViewRecipe(int id)
        {
            Recipe recipe = repository.Recipes.FirstOrDefault(r => r.RecipeID == id);
            return View(recipe);
        }

        
      
       
    }
}
