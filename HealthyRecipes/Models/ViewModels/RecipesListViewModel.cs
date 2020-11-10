using HealthyRecipes.Infrastructure;
using HealthyRecipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyRecipes.Models.ViewModels
{
    public class RecipesListViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
       
    }
}
