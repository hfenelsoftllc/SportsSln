using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportShop.Models;

namespace SportShop
{
    public class Startup
    {
        private IConfiguration _config { get; set; }

        public Startup(IConfiguration config)
        {
            _config=config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<SportShopDbContext>(opts =>{
                opts.UseNpgsql(
                _config["ConnectionStrings:SportShopCon"]);
                });

            services.AddScoped<ISportShopRepository, EFSportShopRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // display detail exception
                app.UseDeveloperExceptionPage();
            }
            //Use the http status code
            app.UseStatusCodePages();

            // to Manage Static file (css,js,image) in wwwroot
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("catpage", "{Category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });                
                endpoints.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage=1 });
                endpoints.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index", productPage=1 });
                endpoints.MapDefaultControllerRoute();

            });
            
            // seed the database with static data
            SeedData.EnsurePopulate(app);
        }
    }
}
