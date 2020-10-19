using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ProductApi.Controllers;
using ProductApi.Models;
using ProductApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductApiTesting
{
    public class Tests
    {
        List<Product> product = new List<Product>();
        //IQueryable<Product> productdata;
        Mock<DbSet<Product>> mockSet;
        Mock<WarehouseManagementContext> productcontextmock;

        [SetUp]
        public void Setup()
        {
            product = new List<Product>()
            {
                new Product{ ProductId =1, ProductName="Dummy1", ProductQuantity=10, ProductCost=10},
                new Product{ ProductId =2, ProductName="Dummy2", ProductQuantity=20, ProductCost=20},
                new Product{ ProductId =3, ProductName="Dummy3", ProductQuantity=30, ProductCost=30}
            };

            var productdata = product.AsQueryable();
            mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productdata.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productdata.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productdata.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(productdata.GetEnumerator());
            var mockContext = new DbContextOptions<WarehouseManagementContext>();
            productcontextmock = new Mock<WarehouseManagementContext>(mockContext);
            productcontextmock.Setup(c => c.Product).Returns(mockSet.Object);
            //context = mockContext.Object;
        }

        [Test]
        public void GetProduct_PassCase()
        {
            var productrepo = new ProductRepository(productcontextmock.Object);
            var productlist = productrepo.GetProduct();
            Assert.AreEqual(3, productlist.Count);
            
            //var mock = new Mock<ProductRepository>(context);
            //ProductController obj = new ProductController(mock.Object);
            //var data = obj.GetProduct();
            //var res = data as ObjectResult;
            //Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void GetProduct_FailCase()
        {
            try
            {
                var productrepo = new ProductRepository(productcontextmock.Object);
                var productlist = productrepo.GetProduct();
                Assert.AreNotEqual(4, productlist.Count);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        //[Test]
        //public void GetProductById_Pass(int id)
        //{
        //    var productrepo = new ProductRepository(productcontextmock.Object);
        //    var controll = new ProductController(productrepo);
        //    var  data = controll.
        //}











        [Test]
        public void DeleteProduct_Pass()
        {
            var productrepo = new ProductRepository(productcontextmock.Object);
            var productlist = productrepo.DeleteProduct(1);
            var res = productlist.IsCompletedSuccessfully;
            Assert.AreEqual(true, res);
        }

        [Test]
        public void DeleteProduct_Fail()
        {
            var productrepo = new ProductRepository(productcontextmock.Object);
            var productlist = productrepo.DeleteProduct(1);
            var res = productlist.IsCompletedSuccessfully;
            Assert.AreNotEqual(false, res);
        }

        //[Test]
        //public void AddProduct(Product product)
        //{
        //    var productrepo = new ProductRepository(productcontextmock.Object);
        //    var productlist = productrepo.AddProduct(product);
        //    Assert.AreEqual(4, productlist.Result);

        //}
    }
}