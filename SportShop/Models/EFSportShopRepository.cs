

using System.Linq;

namespace SportShop.Models{
    public class EFSportShopRepository : ISportShopRepository
    {
        private SportShopDbContext context;
        public EFSportShopRepository(SportShopDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
    }
}