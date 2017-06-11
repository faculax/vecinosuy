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
    public class ServiceControllerTest
    {
        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetServices()
        {
            var allServices = new[]
            {
                new Service()
                {
                    ServiceId = "1",
                    Name = "Service1",
                    Building = "1"
                },
               new Service()
                {
                     ServiceId = "2",
                    Name = "Service2",
                    Building = "1"
                },
            };

            var mockServiceValidator = new Mock<IServiceValidator>();
            mockServiceValidator.Setup(x => x.GetServices()).Returns(allServices);

            var controller = new ServicesController(mockServiceValidator.Object);

            // Arrange            
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");

            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetServices();
            //throw new Exception(actionResult.GetType().ToString());
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Service>>;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void TestPostService()
        {
            Service a = new Service()
            {
                ServiceId = "1",
                Name = "Service1",
                Building = "1"
            };

            var mockServiceValidator = new Mock<IServiceValidator>();
            mockServiceValidator.Setup(x => x.PostService(a)).Verifiable();
            mockServiceValidator.Setup(x => x.secure(null)).Verifiable();
            mockServiceValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new ServicesController(mockServiceValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PostService(a);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Service>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestDeleteService()
        {
            Service a = new Service()
            {
                ServiceId = "1",
                Name = "Service1",
                Building = "1"
            };

            var mockServiceValidator = new Mock<IServiceValidator>();
            mockServiceValidator.Setup(x => x.DeleteService("1")).Verifiable();
            mockServiceValidator.Setup(x => x.secure(null)).Verifiable();
            mockServiceValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new ServicesController(mockServiceValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteService("1");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Service>>;
            Assert.IsNotNull(actionResult);


        }
    }
}
