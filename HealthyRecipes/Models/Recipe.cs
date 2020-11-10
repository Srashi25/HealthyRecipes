using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyRecipes.Models
{
    public class Recipe
    {
        
        public int RecipeID { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public double RecipeAvgRating { get; set; }
        [Required(ErrorMessage = "Please enter Recipe Name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter Recipe Ingreient")]
        public string Ingredient1 { get; set; }
        [Required(ErrorMessage = "Please enter Recipe Ingreient")]
        public string Ingredient2 { get; set; }
        [Required(ErrorMessage = "Please enter Recipe Ingreient")]
        public string Ingredient3 { get; set; }
        [Required(ErrorMessage = "Please enter Recipe Ingreient")]
        public string Ingredient4 { get; set; }
        public string Ingredient5 { get; set; }
        public string Ingredient6 { get; set; }
        [Required(ErrorMessage = "Please enter Instructions")]
        public string Instructions { get; set; }
        [Required(ErrorMessage = "Please enter Image")]
        public string ImageUrl { get; set; }
        public int CookingTime { get; set; }
        [Required(ErrorMessage = "Please Choose Category")]
        public string Category { get; set; }
        public DateTime? LastEdit { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public string Substring(string instructions)
        {
            string substring = Instructions.Substring(0, 60);
            return substring;
        }
        public virtual void AddReview(Review rev)
        {
            rev.Recipe = this;
            Reviews.Add(rev);
        }
        public virtual void DelReview(Review rev)
        {
            if (rev.Recipe == this)
            {
                Reviews.Remove(rev);
            }

        }
    }
}
