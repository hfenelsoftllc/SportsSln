using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;

namespace SportShop.Components
{
    public class NavigationMenuViewComponent:ViewComponent
    {
        private ISportShopRepository repository;

        public NavigationMenuViewComponent(ISportShopRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(repository.Products
                    .Select(x =>x.Category)
                    .Distinct()
                    .OrderBy(x =>x));
        }
    }
}
