using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ShipmentApi.Models;
using ShipmentApi.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ShipmentApiTesting
{
    public class Tests
    {
        List<Shipment> shipment = new List<Shipment>();
        Mock<DbSet<Shipment>> mockSet;
        Mock<WarehouseManagementContext> shipmentcontextmock;

        [SetUp]
        public void Setup()
        {
            shipment = new List<Shipment>()
            {
                new Shipment{ ShipmentId=1,SellerId=1, ProductId=1, Quantity=1, ToLocation="Dummy1", Status="Dummy1" },
                new Shipment{ ShipmentId=2,SellerId=2, ProductId=2, Quantity=2, ToLocation="Dummy2", Status="Dummy2" },
                new Shipment{ ShipmentId=3,SellerId=3, ProductId=3, Quantity=3, ToLocation="Dummy3", Status="Dummy3" }

            };

            var loandata = shipment.AsQueryable();
            mockSet = new Mock<DbSet<Shipment>>();
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.Provider).Returns(loandata.Provider);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.Expression).Returns(loandata.Expression);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.ElementType).Returns(loandata.ElementType);
            mockSet.As<IQueryable<Shipment>>().Setup(m => m.GetEnumerator()).Returns(loandata.GetEnumerator());
            var mockContext = new DbContextOptions<WarehouseManagementContext>();
            shipmentcontextmock = new Mock<WarehouseManagementContext>(mockContext);
            shipmentcontextmock.Setup(c => c.Shipment).Returns(mockSet.Object);
        }

        [Test]
        public void GetShipment_PassCase()
        {
            var shipmentrepo = new ShipmentRepository(shipmentcontextmock.Object);
            var shipmentlist = shipmentrepo.GetShipment();
            Assert.AreEqual(3, shipmentlist.Count());
        }
        [Test]
        public void GetSeller_FailCase()
        {
            var shipmentrepo = new ShipmentRepository(shipmentcontextmock.Object);
            var shipmentlist = shipmentrepo.GetShipment();
            Assert.AreNotEqual(2, shipmentlist.Count());
        }
    }
}