using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyRecipes.Models
{
    public class Review
    {
        
        public int ReviewID { get; set; }
        [Required(ErrorMessage = "Please enter Title!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter Review!")]
        public string ReviewDescription { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeRating { get; set; }
        public string UserId { get; set; }
    }
}
