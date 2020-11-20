using Microsoft.EntityFrameworkCore;


namespace SportShop.Models{

    public class SportShopDbContext: DbContext{
        public SportShopDbContext(DbContextOptions<SportShopDbContext> options)
                :base(options)
        {            
        }

        public DbSet<Product> Products{get;set;}
    }
}