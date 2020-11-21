using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportShop.Components;
using SportShop.Controllers;
using SportShop.Models;
using SportShop.Models.ViewModels;
using Xunit;

namespace SportShop.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Select_Category()
        {
            // I assert that the duplicates are removed and that alphabetical ordering is imposed

            //Arrange
            Mock<ISportShopRepository> mock = new Mock<ISportShopRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]{
                new Product{ProductID=1, Name="P1",Category="Apples" },
                new Product{ProductID=2, Name="P2",Category="Apples" },
                new Product{ProductID=3, Name="P3",Category="Plume" },
                new Product{ProductID=3, Name="P4",Category="Lemon" },
                new Product{ProductID=3, Name="P5",Category="Plume" },
                new Product{ProductID=4, Name="P6",Category="Orange" },
                new Product{ProductID=3, Name="P7",Category="Orange" },
                new Product{ProductID=5, Name="P8",Category="Strawberry" },
                //new Product{ProductID=6, Name="P6" }
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            //target.PageSize = 3;

            //Act
            string[] results =((IEnumerable<string>)(target.Invoke() as ViewComponentResult).ViewData.Model).ToArray();

            //Assert
          
            Assert.True(Enumerable.SequenceEqual(new string[] {"Apples", "Lemon","Plume","Orange","Strawberry" }, results));
            
        }

        [Fact]
        public void Can_Use_Repository()
        {
            //Arrange
            Mock<ISportShopRepository> mock = new Mock<ISportShopRepository>();

            mock.Setup(m =>m.Products).Returns((new Product[]{
                new Product{ProductID=1, Name="P1",Category="Cat1" },
                new Product{ProductID=2, Name="P2",Category="Cat2" }
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);

            //Act
            ProductsListViewModel  result=
                 controller.Index(null).ViewData.Model as ProductsListViewModel;

            //Assert
            Product [] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length==2);
            Assert.Equal("P1",prodArray[0].Name);
            Assert.Equal("P2",prodArray[1].Name);

        }

        [Fact]
        public void Can_Paginate(){
            //Arrange
            Mock<ISportShopRepository> mock = new Mock<ISportShopRepository>();
            mock.Setup(m =>m.Products).Returns((new Product[]{
                new Product{ProductID=1, Name="P1",Category="Cat1" },
                new Product{ProductID=2, Name="P2",Category="Cat2" },
                new Product{ProductID=3, Name="P3",Category="Cat3" },
                new Product{ProductID=4, Name="P4",Category="Cat4" },
                new Product{ProductID=5, Name="P5",Category="Cat5" },
                //new Product{ProductID=6, Name="P6" }
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize =3;

            //Act
            ProductsListViewModel result=
                 controller.Index(null,2).ViewData.Model as ProductsListViewModel;

            //Assert
            Product [] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4",prodArray[0].Name);
            Assert.Equal("P5",prodArray[1].Name);
        }
        [Fact]
        public void Can_Send_Pagination_View_Model(){
            //Arrange
             Mock<ISportShopRepository> mock = new Mock<ISportShopRepository>();
            mock.Setup(m =>m.Products).Returns((new Product[]{
                new Product{ProductID=1, Name="P1",Category="Cat1" },
                new Product{ProductID=2, Name="P2",Category="Cat2" },
                new Product{ProductID=3, Name="P3",Category="Cat3" },
                new Product{ProductID=4, Name="P4",Category="Cat4" },
                new Product{ProductID=5, Name="P5",Category="Cat5" }
                //new Product{ProductID=6, Name="P6" }
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object){PageSize =3};            

            //Act
            ProductsListViewModel result=
                 controller.Index(null,2).ViewData.Model as ProductsListViewModel;

            //Assert
           PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages); 
        }
    }

}
