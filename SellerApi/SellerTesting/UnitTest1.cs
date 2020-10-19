using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SellerApi.Controllers;
using SellerApi.Models;
using SellerApi.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SellerTesting
{
    public class Tests
    {
        List<Seller> seller = new List<Seller>();
        Mock<DbSet<Seller>> mockSet;
        Mock<WarehouseManagementContext> sellercontextmock;

        [SetUp]
        public void Setup()
        {
            seller = new List<Seller>()
            {
                new Seller{ SellerId = 1, SellerName="Dummy1", Address="Dummy1", Phone="1", IsAvailable="Yes"},
                new Seller{ SellerId = 2, SellerName="Dummy2", Address="Dummy2", Phone="2", IsAvailable="Yes"},
                new Seller{ SellerId = 3, SellerName="Dummy3", Address="Dummy3", Phone="3", IsAvailable="Yes"}
            };

            var loandata = seller.AsQueryable();
            mockSet = new Mock<DbSet<Seller>>();
            mockSet.As<IQueryable<Seller>>().Setup(m => m.Provider).Returns(loandata.Provider);
            mockSet.As<IQueryable<Seller>>().Setup(m => m.Expression).Returns(loandata.Expression);
            mockSet.As<IQueryable<Seller>>().Setup(m => m.ElementType).Returns(loandata.ElementType);
            mockSet.As<IQueryable<Seller>>().Setup(m => m.GetEnumerator()).Returns(loandata.GetEnumerator());
            var mockContext = new DbContextOptions<WarehouseManagementContext>();
            sellercontextmock = new Mock<WarehouseManagementContext>(mockContext);
            sellercontextmock.Setup(c => c.Seller).Returns(mockSet.Object);
        }

        [Test]
        public void GetSeller_PassCase()
        {
            var sellerrepo = new SellerRepository(sellercontextmock.Object);
            var sellerlist = sellerrepo.GetSeller();
            Assert.AreEqual(3, sellerlist.Count);
        }
        [Test]
        public void GetSeller_FailCase()
        {
            var sellerrepo = new SellerRepository(sellercontextmock.Object);
            var sellerlist = sellerrepo.GetSeller();
            Assert.AreNotEqual(2, sellerlist.Count);
        }
    }
}