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
    public class AccountStateControllerTest
    {
        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetAccountStates()
        {
            var allAccountStates = new[]
            {
                new AccountState()
                {
                    UserId = "luda@gmail.com",
                    Month = 10,
                    Year = 2016,
                    Ammount = 1500, 
                    Deleted = false
                }
            };

            var mockAccountStateValidator = new Mock<IAccountStateValidator>();
            mockAccountStateValidator.Setup(x => x.GetAccountStates()).Returns(allAccountStates);

            var controller = new AccountStatesController(mockAccountStateValidator.Object);

            // Arrange            
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");

            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetAccountStates();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AccountState>>;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetAccountState()
        {
            new AccountState()
            {
                UserId = "luda@gmail.com",
                Month = 10,
                Year = 2016,
                Ammount = 1500,
                Deleted = false
            };

            var mockAccountStateValidator = new Mock<IAccountStateValidator>();

            var controller = new AccountStatesController(mockAccountStateValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetAccountState();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AccountState>>;
            Assert.IsNotNull(actionResult);


        }

        [TestMethod]
        public void TestPutAccountStates()
        {

            AccountState accountState = new AccountState()
            {
                UserId = "luda@gmail.com",
                Month = 10,
                Year = 2016,
                Ammount = 1500,
                Deleted = false
            };


            var mockAccountStateValidator = new Mock<IAccountStateValidator>();
            mockAccountStateValidator.Setup(x => x.PutAccountState(1, accountState)).Returns(accountState);
            mockAccountStateValidator.Setup(x => x.secure(null)).Verifiable();
            mockAccountStateValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new AccountStatesController(mockAccountStateValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PutAccountState(1, accountState);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AccountState>>;
            Assert.IsNotNull(actionResult);


        }

        [TestMethod]
        public void TestPostAccountState()
        {

            AccountState accountState = new AccountState()
            {
                UserId = "luda@gmail.com",
                Month = 10,
                Year = 2016,
                Ammount = 1500,
                Deleted = false
            };

            var mockAccountStateValidator = new Mock<IAccountStateValidator>();
            mockAccountStateValidator.Setup(x => x.PostAccountState(accountState)).Verifiable();
            mockAccountStateValidator.Setup(x => x.secure(null)).Verifiable();
            mockAccountStateValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new AccountStatesController(mockAccountStateValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PostAccountState(accountState);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<AccountState>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestDeleteAccountState()
        {
            AccountState accountState = new AccountState()
            {
                UserId = "luda@gmail.com",
                Month = 10,
                Year = 2016,
                Ammount = 1500,
                Deleted = false
            };

            var mockAccountStateValidator = new Mock<IAccountStateValidator>();
            mockAccountStateValidator.Setup(x => x.DeleteAccountState("luda@gmail.com", 10, 2016)).Verifiable();
            mockAccountStateValidator.Setup(x => x.secure(null)).Verifiable();
            mockAccountStateValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new AccountStatesController(mockAccountStateValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteAccountState("luda@gmail.com", 10, 2016);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<User>>;
            Assert.IsNotNull(actionResult);


        }
    }
}
