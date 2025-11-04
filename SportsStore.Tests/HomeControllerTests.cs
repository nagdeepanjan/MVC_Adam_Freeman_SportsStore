using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] { new Product {ProductID = 1, Name = "P1"}, new Product {ProductID = 2, Name = "P2"}}).AsQueryable<Product>());
            
            HomeController controller = new HomeController(mock.Object);
            
            // Act
            IEnumerable<Product>? result = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;
            // Assert
            Product[] prodArray = result?.ToArray() ?? Array.Empty<Product>();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }

        [Fact]
        public void Can_Paginate_Products()
        {
            // Arrange
            var products = new[]
            {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
                new Product { ProductID = 4, Name = "P4" },
                new Product { ProductID = 5, Name = "P5" },
                new Product { ProductID = 6, Name = "P6" }
            };
            var mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns(products.AsQueryable());
            var controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            // Act
            var result = (controller.Index(productPage: 2) as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // Assert
            var prodArray = result?.ToArray() ?? Array.Empty<Product>();
            Assert.Equal(3, prodArray.Length); // PageSize is 3, so page 2 should have 3 products
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
            Assert.Equal("P6", prodArray[2].Name);
        }
    }
}
