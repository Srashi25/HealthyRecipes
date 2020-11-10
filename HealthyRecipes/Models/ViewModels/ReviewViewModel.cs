using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyRecipes.Models.ViewModels
{
    public class ReviewViewModel
    {
        public int RecipeID { get; set; }
        public Review Review { get; set; }
    }
}
