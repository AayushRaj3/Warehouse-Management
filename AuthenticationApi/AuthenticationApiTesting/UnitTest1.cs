using AuthenticationApi.Controllers;
using AuthenticationApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AuthenticationApiTesting
{
    public class Tests
    {
        List<UserInfo> user = new List<UserInfo>();
        IQueryable<UserInfo> userdata;
        Mock<DbSet<UserInfo>> mockSet;
        Mock<WarehouseManagementContext> usercontextmock;
        
        
        [SetUp]
        public void Setup()
        {
            user = new List<UserInfo>()
            {
                new UserInfo{ UserId=1, Username="ABC", Password="123"}
            };

            userdata = user.AsQueryable();
            mockSet = new Mock<DbSet<UserInfo>>();
            mockSet.As<IQueryable<UserInfo>>().Setup(m => m.Provider).Returns(userdata.Provider);
            mockSet.As<IQueryable<UserInfo>>().Setup(m => m.Expression).Returns(userdata.Expression);
            mockSet.As<IQueryable<UserInfo>>().Setup(m => m.ElementType).Returns(userdata.ElementType);
            mockSet.As<IQueryable<UserInfo>>().Setup(m => m.GetEnumerator()).Returns(userdata.GetEnumerator());
            var mockContext = new DbContextOptions<WarehouseManagementContext>();
            usercontextmock = new Mock<WarehouseManagementContext>(mockContext);
            usercontextmock.Setup(c => c.UserInfo).Returns(mockSet.Object);

        }

        [Test]
        public void Login_Pass()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("RandomTextABCDEFGHIJKLM");
            var controller = new TokenController(config.Object, usercontextmock.Object);
            var auth = controller.LoginResult(new UserInfo { UserId = 1, Username = "ABC", Password = "123" }) as ObjectResult;
            Assert.AreEqual(200, auth.StatusCode);
        }

        [Test]
        public void Login_Fail()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("RandomTextABCDEFGHIJKLM");
            var controller = new TokenController(config.Object, usercontextmock.Object);
            var auth = controller.LoginResult(new UserInfo { UserId = 1, Username = "ABC", Password = "23" }) as ObjectResult;
            Assert.IsNull(auth);
        }
    }
}