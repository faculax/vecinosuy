using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VecinosUY.Data.Entities;
using VecinosUY.Exceptions;
using VecinosUY.Logic;
using Moq;
using VecinosUY.Data.Repository;
using VecinosUY.Web.Api.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Linq;
using VecinosUY.Factory;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace VecinosUY.Test
{
    [TestClass]
    public class BuildingControllerTest
    {
        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetBuildings()
        {
            var allBuildings = new[]
            {
                new Building()
                {
                    BuildingId = "1",
                    Name = "Building1",
                    Address = "Address",
                    Admin = "1"
                },
               new Building()
                {
                    BuildingId = "2",
                    Name = "Building2",
                    Address = "Address",
                    Admin = "2"
                },
            };

            var mockBuildingValidator = new Mock<IBuildingValidator>();
            mockBuildingValidator.Setup(x => x.GetBuildings()).Returns(allBuildings);

            var controller = new BuildingsController(mockBuildingValidator.Object);

            // Arrange            
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");

            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetBuildings();
            //throw new Exception(actionResult.GetType().ToString());
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Building>>;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetBuilding()
        {
            new Building()
            {
                BuildingId = "1",
                Name = "Building1",
                Address = "Address",
                Admin = "1"
            };

            var mockBuildingValidator = new Mock<IBuildingValidator>();

            var controller = new BuildingsController(mockBuildingValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetBuilding("1");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Building>>;
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void TestPostBuilding()
        {
            Building a = new Building()
            {
                BuildingId = "1",
                Name = "Building1",
                Address = "Address",
                Admin = "1"
            };

            var mockBuildingValidator = new Mock<IBuildingValidator>();
            mockBuildingValidator.Setup(x => x.PostBuilding(a)).Verifiable();
            mockBuildingValidator.Setup(x => x.secure(null)).Verifiable();
            mockBuildingValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new BuildingsController(mockBuildingValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PostBuilding(a);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Building>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestDeleteBuilding()
        {
            Building a = new Building()
            {
                BuildingId = "1",
                Name = "Building1",
                Address = "Address",
                Admin = "1"
            };

            var mockBuildingValidator = new Mock<IBuildingValidator>();
            mockBuildingValidator.Setup(x => x.DeleteBuilding("1")).Verifiable();
            mockBuildingValidator.Setup(x => x.secure(null)).Verifiable();
            mockBuildingValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new BuildingsController(mockBuildingValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteBuilding("1");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Building>>;
            Assert.IsNotNull(actionResult);


        }
    }
}
