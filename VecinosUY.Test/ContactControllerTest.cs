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
    public class ContactControllerTest
    {
        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetContacts()
        {
            var allContacts = new[]
            {
                new Contact()
                {
                    ContactId = "luda@gmail.com",
                    Name = "Luda",
                    Phone = "091123456",
                    Apartment = "123"
                },
                new Contact()
                {
                    ContactId = "facu@hotmail.com",
                    Name = "Facu",
                    Phone = "091111111",
                    Apartment = "101"
                },
            };
            
            var mockContactValidator = new Mock<IContactValidator>();
            mockContactValidator.Setup(x => x.GetContacts()).Returns(allContacts);

            var controller = new ContactController(mockContactValidator.Object);

            // Arrange            
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");

            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetContacts();
            //throw new Exception(actionResult.GetType().ToString());
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Contact>>;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetContact()
        {

            Contact c = new Contact()
            {
                ContactId = "luda@gmail.com",
                Name = "Luda",
                Phone = "091123456",
                Apartment = "123"
            };

            var mockContactValidator = new Mock<IContactValidator>();
            //mockContactValidator.Setup(x => x.GetContact("1")).Returns();
            //mockContactValidator.Setup(x => x.secure(null)).Verifiable();
            //mockContactValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new ContactController(mockContactValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetContact("1");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Contact>>;
            Assert.IsNotNull(actionResult);


        }

        [TestMethod]
        public void TestPutContact()
        {

            Contact c = new Contact()
            {
                ContactId = "luda@gmail.com",
                Name = "Luda",
                Phone = "091123456",
                Apartment = "123"
            };


            var mockContactValidator = new Mock<IContactValidator>();
            mockContactValidator.Setup(x => x.PutContact("1", c)).Returns(c);
            mockContactValidator.Setup(x => x.secure(null)).Verifiable();
            mockContactValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new ContactController(mockContactValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PutContact("1", c);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Contact>>;
            Assert.IsNotNull(actionResult);


        }

        [TestMethod]
        public void TestPostContact()
       {

        Contact c = new Contact()
        {
            ContactId = "luda@gmail.com",
            Name = "Luda",
            Phone = "091123456",
            Apartment = "123"
        };

            var mockContactValidator = new Mock<IContactValidator>();
            mockContactValidator.Setup(x => x.PostContact(c)).Verifiable();
            mockContactValidator.Setup(x => x.secure(null)).Verifiable();
            mockContactValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new ContactController(mockContactValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PostContact(c);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Contact>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestDeleteContact()
        {

            Contact c = new Contact()
            {
                ContactId = "luda@gmail.com",
                Name = "Luda",
                Phone = "091123456",
                Apartment = "123"
            };

            var mockContactValidator = new Mock<IContactValidator>();
            mockContactValidator.Setup(x => x.DeleteContact("1")).Verifiable();
            mockContactValidator.Setup(x => x.secure(null)).Verifiable();
            mockContactValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new ContactController(mockContactValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteContact("1");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Contact>>;
            Assert.IsNotNull(actionResult);


        }
    }
}
