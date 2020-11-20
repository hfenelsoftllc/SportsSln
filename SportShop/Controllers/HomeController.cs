using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using SportShop.Models.ViewModels;

namespace SportShop.Controllers{

    public class HomeController:Controller{

        private ISportShopRepository repository;

        public int PageSize = 4;

        public HomeController(ISportShopRepository repo)
        {
            repository = repo;
        }



        public ViewResult Index(string category, int productPage = 1) =>
            View(
                new ProductsListViewModel {
                    Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Products.Count()
                    },
                    CurrencyCategory = category

                }); 
    }
}