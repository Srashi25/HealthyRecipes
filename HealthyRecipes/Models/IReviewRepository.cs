using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyRecipes.Models
{
    public interface IReviewRepository
    {
        IQueryable<Review> Reviews { get; }
       
    }
}
