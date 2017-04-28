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
    public class UserTest
    {
        //[TestMethod]
        ////[ExpectedException(typeof(NotExistException))]
        //public void TestLogin()
        //{
        //    var allUsers = new[]
        //    {
        //        new User()
        //        {
        //            UserId = 1,
        //            Name = "luis",
        //            Admin = true,
        //            Deleted = false,
        //            Password = "luis"
        //        },
        //        new User()
        //        {
        //            UserId = 2,
        //            Name = "facundo",
        //            Admin = true,
        //            Deleted = false,
        //            Password = "facundo"
        //        }
        //};
        //    var mockUserValidator = new Mock<IUserValidator>();
        //    mockUserValidator.Setup(x => x.GetUsers()).Returns(allUsers);

        //    var controller = new UsersController(mockUserValidator.Object);

        //    IHttpActionResult actionResult = controller.GetUsers();
        //    OkNegotiatedContentResult<IEnumerable<UserDTO>> contentResult = (OkNegotiatedContentResult<IEnumerable<UserDTO>>)actionResult;
        //    //Assert.IsInstanceOfType(actionResult, OkNegotiatedContentResult<IEnumerable<UserDTO>>);
        //    Assert.IsNotNull(contentResult);
        //    Assert.IsNotNull(contentResult.Content);
        //    Assert.AreSame(allUsers, contentResult.Content);

        //}
        

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetUsers()
        {
            var allUsers = new[]
            {
                new User()
                {
                    UserId = "1",
                    Name = "luis",
                    Admin = true,
                    Deleted = false,
                    Password = "luis"
                },
                new User()
                {
                    UserId = "2",
                    Name = "facundo",
                    Admin = true,
                    Deleted = false,
                    Password = "facundo"
                }
            };
                var allUsersDTO = new[]
                {
                    new UserDTO()
                    {
                        UserId = "1",
                        Name = "luis",
                        Admin = true,
                        Deleted = false                    
                    },
                    new UserDTO()
                    {
                        UserId = "2",
                        Name = "facundo",
                        Admin = true,
                        Deleted = false
                    }
            };
            var mockUserValidator = new Mock<IUserValidator>();
            mockUserValidator.Setup(x => x.GetUsers()).Returns(allUsers);
            mockUserValidator.Setup(x => x.secure(null)).Verifiable();
            mockUserValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new UsersController(mockUserValidator.Object);

            //controller.ControllerContext.Request.Headers.Add("TODO_PAGOS_TOKEN","luis");
            //controller.Request.Content.Headers = new System.Net.Http.Headers.HttpContentHeaders
            //controller.Request.Headers.Add("TODO_PAGOS_TOKEN", "1");

            // Arrange            
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");

            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;


            //controller.Request.Content.Headers.Add("TODO_PAGOS_TOKEN", "1");
            //HttpContextFactory.Current.Request.Headers.Add("TODO_PAGOS_TOKEN", "luis");
            //controller.ControllerContext.

            IHttpActionResult actionResult = controller.GetUsers();
            //throw new Exception(actionResult.GetType().ToString());
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<UserDTO>>;

            //IEnumerable<UserDTO> contentResult = (IEnumerable<UserDTO>)actionResult;
            //Assert.IsInstanceOfType(actionResult, OkNegotiatedContentResult<IEnumerable<UserDTO>>);
            
            
            Assert.IsNotNull(contentResult);

            //Assert.IsNotNull(contentResult.Content);
            //Assert.AreSame(allUsers, contentResult.Content);
            

        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetUser()
        {

            User u = new User()
            {
                UserId = "1",
                Name = "luis",
                Admin = true,
                Deleted = false,
                Password = "luis"
            };

            var mockUserValidator = new Mock<IUserValidator>();
            mockUserValidator.Setup(x => x.GetUser("1")).Returns(u);
            mockUserValidator.Setup(x => x.secure(null)).Verifiable();
            mockUserValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new UsersController(mockUserValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetUser("1");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<UserDTO>>;
            Assert.IsNotNull(actionResult);


        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestLogin()
        {

            User u = new User()
            {
                UserId = "1",
                Name = "luis",
                Admin = true,
                Deleted = false,
                Password = "luis"
            };

            var mockUserValidator = new Mock<IUserValidator>();
            mockUserValidator.Setup(x => x.LogIn("1","luis")).Returns(u);
            mockUserValidator.Setup(x => x.secure(null)).Verifiable();
            mockUserValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new UsersController(mockUserValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.LogIn("1", "luis");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<User>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestPutUser()
        {

            User u = new User()
            {
                UserId = "1",
                Name = "luis",
                Admin = true,
                Deleted = false,
                Password = "luis"
            };

            UserDTO udto = new UserDTO()
            {
                UserId = "2",
                Name = "facundo",
                Admin = true,
                Deleted = false
            };
            
            var mockUserValidator = new Mock<IUserValidator>();
            mockUserValidator.Setup(x => x.PutUser("1",u)).Returns(u);
            mockUserValidator.Setup(x => x.secure(null)).Verifiable();
            mockUserValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new UsersController(mockUserValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PutUser("1",u);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<User>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestPostUser()
        {

            User u = new User()
            {
                UserId = "1",
                Name = "luis",
                Admin = true,
                Deleted = false,
                Password = "luis"
            };

            UserDTO udto = new UserDTO()
            {
                UserId = "2",
                Name = "facundo",
                Admin = true,
                Deleted = false
            };

            var mockUserValidator = new Mock<IUserValidator>();
            mockUserValidator.Setup(x => x.PostUser(u)).Verifiable();
            mockUserValidator.Setup(x => x.secure(null)).Verifiable();
            mockUserValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new UsersController(mockUserValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PostUser(u);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<User>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestDeleteUser()
        {

            User u = new User()
            {
                UserId = "1",
                Name = "luis",
                Admin = true,
                Deleted = false,
                Password = "luis"
            };

            UserDTO udto = new UserDTO()
            {
                UserId = "2",
                Name = "facundo",
                Admin = true,
                Deleted = false
            };

            var mockUserValidator = new Mock<IUserValidator>();
            mockUserValidator.Setup(x => x.DeleteUser("1")).Verifiable();
            mockUserValidator.Setup(x => x.secure(null)).Verifiable();
            mockUserValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new UsersController(mockUserValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteUser("1");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<User>>;
            Assert.IsNotNull(actionResult);


        }
    }
}
