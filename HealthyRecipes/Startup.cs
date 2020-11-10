using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyRecipes.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace HealthyRecipes
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
            Configuration["Data:HealthyRecipesSite:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(
            Configuration["Data:HealthyRecipesIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();
            services.AddTransient<IRecipeRepository, EFRecipeRepository>();
            services.AddTransient<IReviewRepository, EFReviewRepository>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "",
                 template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                 name: "",
                 template: "{controller=Account}/{action=Login}/{id?}");
                routes.MapRoute(
                 name: "",
                 template: "{controller}/{action}/{id?}");
                routes.MapRoute(
               name: null,
               template: "Page{recipePage:int}",
               defaults: new
               {
                   controller = "Admin",
                   action = "DataPage",
                   recipePage = 1
               }
               );
                
                routes.MapRoute(
                name: null,
                template: "{category}/Page{recipePage:int}",
                defaults: new { controller = "Home", action = "RecipesList" }
                );

                routes.MapRoute(
                name: null,
                template: "Page{recipePage:int}",
                defaults: new
                {
                controller = "Admin",
                action = "DataPage",
                productPage = 1
                }
                );
                routes.MapRoute(
                name: null,
                template: "{category}",
                defaults: new
                {
                    controller = "Admin",
                    action = "DataPage",
                    recipePage = 1
                }
                );
                routes.MapRoute(
                name: null,
                template: "",
                defaults: new
                {
                    controller = "Admin",
                    action = "DataPage",
                    recipePage = 1
                });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
                routes.MapRoute(
                name: null,
                template: "",
                defaults: new { controller = "Admin", action = "DataPage", recipePage = 1 });

                routes.MapRoute(
                name: "pagination",
                template: "Recipes/Page{recipePage}",
                defaults: new { Controller = "Admin", action = "DataPage" });

            });
            SeedData.EnsurePopulated(app);
          IdentitySeedData.EnsurePopulated(app);

        }
    }
}
