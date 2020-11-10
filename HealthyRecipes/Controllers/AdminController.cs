using HealthyRecipes.Models;
using HealthyRecipes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HealthyRecipes.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IRecipeRepository repository;
        private UserManager<IdentityUser> userManager;
        IReviewRepository reviewRepository;
        private int PageSize = 3;
        public AdminController(IRecipeRepository repo, IReviewRepository reviewRepo,UserManager<IdentityUser> userMan)
        {
            repository = repo;
            userManager = userMan;
            reviewRepository = reviewRepo;
        }
    
        public IActionResult DataPage(string category, int recipePage = 1)
        {
          return  View(new RecipesListViewModel
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
                CurrentCategory = category,
        });
            
        }
        public async Task<IActionResult> Edit(int recipeId)
        {
            Recipe recipe = repository.Recipes.FirstOrDefault(r => r.RecipeID == recipeId);
            var LoggedUser = await userManager.GetUserAsync(User);
            if (recipe.UserId == LoggedUser.UserName)
            {
                return View(recipe);
            }
            else
            {
                TempData["message"] = $"You are not allowed to edit";
                return RedirectToAction("DataPage");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Recipe recipe)
        {
            var LoggedUser = await userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                recipe.LastEdit = DateTime.Now;
                recipe.UserId = LoggedUser.UserName;
                repository.SaveRecipe(recipe);
                TempData["message"] = $"{recipe.Name} Last updated at {recipe.LastEdit} by {recipe.UserId}";
                return RedirectToAction("DataPage");
            }
            else
            {
                return View(recipe);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int recipeId)
        {
            Recipe recipe = repository.Recipes.FirstOrDefault(r => r.RecipeID == recipeId);
            var LoggedUser = await userManager.GetUserAsync(User);
            if (recipe.UserId == LoggedUser.UserName)
            {
                Recipe deletedRecipe = repository.DeleteRecipe(recipeId);
                if (deletedRecipe != null)
                {
                    TempData["message"] = $"{deletedRecipe.Name} was deleted";
                }
            }
            else
            {
                TempData["message"] = $"You are not allowed to delete!";
            }
            return RedirectToAction("DataPage");
        }

        public ViewResult DisplayPage(int id)
        {
            Recipe recipe = repository.Recipes.FirstOrDefault(r => r.RecipeID == id);
            return View(recipe);
        }
        [HttpGet]
        public async Task<IActionResult> AddRecipe()
        {
            var LoggedUser = await userManager.GetUserAsync(User);
            Recipe recipe = new Recipe
            {
                UserId = LoggedUser.UserName
            };
            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(Recipe recipe)
        {
            var LoggedUser = await userManager.GetUserAsync(User);
            recipe.Date = DateTime.Now;
            recipe.UserId = LoggedUser.UserName;
            repository.SaveRecipe(recipe);
            TempData["message"] = $"{recipe.Name} was added at {recipe.Date}";
            return View("RecipeAdded", recipe);

        }
        public ViewResult InsertPage()
        {
            return View("InsertPage");
        }
        [HttpGet]
        public async Task<IActionResult> AddReview(int id)
        {
            var LoggedUser = await userManager.GetUserAsync(User);
            Recipe recipe = repository.Recipes.FirstOrDefault(r => r.RecipeID == id && r.UserId != LoggedUser.UserName);
            if (recipe != null)
            {
                ReviewViewModel rvm = new ReviewViewModel()
                {
                    RecipeID = id,
                    Review = new Review() { UserId = LoggedUser.UserName}
                };
                return View(rvm);
            }
            else
            {
                TempData["message"] = $"{LoggedUser.UserName}, you can't add Review to your own recipe!";
                return RedirectToAction("DataPage");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewViewModel reviewViewModel)
        {
            if (ModelState.IsValid)
            {
                var LoggedUser = await userManager.GetUserAsync(User);

                Recipe recipe = repository.Recipes.FirstOrDefault(r => r.RecipeID == reviewViewModel.RecipeID);
                reviewViewModel.Review.UserId = LoggedUser.UserName;
                recipe.AddReview(reviewViewModel.Review);
                double calculateAvgRate = 0;
                int sumRate = 0;
                foreach (var rating in recipe.Reviews)
                {
                    sumRate = sumRate + rating.RecipeRating;
                    calculateAvgRate++;
                }
                recipe.RecipeAvgRating = sumRate / calculateAvgRate;
                repository.SaveRecipe(recipe);
                return View("DisplayPage", recipe);
            }
            else
            {
                return View(reviewViewModel);
            }
        }

        [HttpGet]
        public IActionResult EditReview(int reviewId)
        {
            Review review = reviewRepository.Reviews.FirstOrDefault(r => r.ReviewID == reviewId);          
            ReviewViewModel rvm = new ReviewViewModel
            {
                RecipeID = review.Recipe.RecipeID,
                Review = review
                
            };
            return View(rvm);
        }


        [HttpPost]
        public async Task<IActionResult> EditReview(ReviewViewModel reviewViewModel)
        {
            Recipe recipe = repository.Recipes.FirstOrDefault(r => r.RecipeID == reviewViewModel.RecipeID);
            if (ModelState.IsValid)
            {
                var LoggedUser = await userManager.GetUserAsync(User);
                reviewViewModel.Review.Recipe = recipe;
                reviewViewModel.Review.UserId = LoggedUser.UserName;
                repository.EditReview(reviewViewModel.Review);
                TempData["message"] = $"{reviewViewModel.Review.Title} was Saved";
                return View("DisplayPage", recipe);
            }
            else
            {
                return View(reviewViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DelReview(int recipeId,int reviewId)
        {
            Recipe recipe = repository.Recipes.FirstOrDefault(r => r.RecipeID == recipeId);
            Review review = recipe.Reviews.FirstOrDefault(r=> r.ReviewID==reviewId);
            var LoggedUser = await userManager.GetUserAsync(User);
            if (recipe.UserId == LoggedUser.UserName || review.UserId == LoggedUser.UserName)
            { 
                repository.DelReview(review);
                if (review != null)
                {
                    TempData["message"] = $"{review.Title} was deleted";
                }
            }
            else
            {
                TempData["message"] = $"You are not allowed to delete review!";
            }
           
            return RedirectToAction("DataPage");
        }


    }
}
