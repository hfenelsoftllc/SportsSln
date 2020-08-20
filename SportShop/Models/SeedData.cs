

using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportShop.Models{
    public static class SeedData{
        public static void EnsurePopulate(IApplicationBuilder app){
            SportShopDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<SportShopDbContext>();

            if(context.Database.GetPendingMigrations().Any()){
                    context.Database.Migrate();
            }

            if(!context.Products.Any()){
                context.Products.AddRange(
                    new Product{
                        Name="Kayak", Price=275, Category="WaterSport", Description="Boat for single person"
                    },
                    new Product{
                        Name="LifeJacket", Price=48.95m, Category="WaterSport", Description="Sport Protective gear and fashionable"
                    },
                    new Product{
                        Name="Stadium", Price=79500, Category="Soccer", Description="Flat-pack 35000 seat Stadium"
                    },
                    new Product{
                        Name="Thinking Cap", Price=16, Category="Chess", Description="Improve brain efficiency by 75%"
                    },
                    new Product{
                        Name="Unsteady Chair", Price=29.95m, Category="Chess", Description="Secretly give your opponent disavantage"
                    },
                    new Product{
                        Name="Human chess Board", Price=75, Category="Chess", Description="A fun game for family"
                    },
                    new Product{
                        Name="Bling Bling King", Price=1200, Category="Chess", Description="Gold plate, diamond-studded King"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}