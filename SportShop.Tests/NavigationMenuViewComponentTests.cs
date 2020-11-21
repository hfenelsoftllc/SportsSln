using Microsoft.AspNetCore.Mvc;
using Moq;
using SportShop.Components;
using SportShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportShop.Tests
{
    public class NavigationMenuViewComponentTests
    {

        [Fact]
        public void Indicates_Selected_Category()
        {
            //Arrange
            string categoryToSelect = "Apples";

            Mock<ISportShopRepository> mock = new Mock<ISportShopRepository>();

            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product{ProductID=1, Name="P1",Category="Apples" },
                new Product{ProductID=4, Name="P2",Category="Lemon" },
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new Microsoft.AspNetCore.Mvc.ViewComponents.ViewComponentContext
            {
                ViewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };

            target.RouteData.Values["category"] = categoryToSelect;


            //Act
            string result = (string)(target.Invoke() as ViewComponentResult).ViewData["SelectedCategory"];

            //Assert

            Assert.Equal(categoryToSelect, result);
        }


        [Fact]
        public void Can_Select_Categories()
        {
            // I assert that the duplicates are removed and that alphabetical ordering is imposed

            //Arrange
            Mock<ISportShopRepository> mock = new Mock<ISportShopRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]{
                new Product{ProductID=1, Name="P1",Category="Apples" },
                new Product{ProductID=2, Name="P2",Category="Apples" },
                new Product{ProductID=3, Name="P3",Category="Plums" },
                new Product{ProductID=4, Name="P4",Category="Oranges" },
                //new Product{ProductID=5, Name="P5",Category="Plume" },
                //new Product{ProductID=6, Name="P6",Category="Oranges" },
                //new Product{ProductID=7, Name="P7",Category="Oranges" },
                //new Product{ProductID=8, Name="P8",Category="Strawberry" },
                //new Product{ProductID=6, Name="P6" }
            }).AsQueryable<Product>());


            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            //Act
            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewComponentResult).ViewData.Model).ToArray();

            //Assert

            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));

        }


    }
}
