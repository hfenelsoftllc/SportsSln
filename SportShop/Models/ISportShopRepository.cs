
using System.Linq;

namespace SportShop.Models{

    public interface ISportShopRepository
    {
         IQueryable<Product>Products{get;}
    }

}