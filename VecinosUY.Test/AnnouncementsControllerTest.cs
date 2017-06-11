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
    public class AnnouncementsControllerTest
    {
        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetAnnouncements()
        {
            var allAnnouncements = new[]
            {
                new Announcement()
                {
                    AnnouncementId = 1,
                    Title = "Announcement1",
                    Body = "Body1",
                    Deleted = false, 
                    Image = null
                },
                  new Announcement()
                {
                    AnnouncementId = 2,
                    Title = "Announcement2",
                    Body = "Body2",
                    Deleted = false,
                    Image = null
                },
            };

            var mockAnnouncementValidator = new Mock<IAnnouncementValidator>();
            mockAnnouncementValidator.Setup(x => x.GetAnnouncements()).Returns(allAnnouncements);

            var controller = new AnnouncementsController(mockAnnouncementValidator.Object);
            
            // Arrange            
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");

            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetAnnouncements();
            //throw new Exception(actionResult.GetType().ToString());
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Announcement>>;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetAnnouncement()
        {
            new Announcement()
            {
                AnnouncementId = 1,
                Title = "Announcement1",
                Body = "Body1",
                Deleted = false,
                Image = null
            };

            var mockAnnouncementValidator = new Mock<IAnnouncementValidator>();

            var controller = new AnnouncementsController(mockAnnouncementValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetAnnouncement(1);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Announcement>>;
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void TestPutAnnouncement()
        {

            Announcement a = new Announcement()
            {
                AnnouncementId = 1,
                Title = "Announcement1",
                Body = "Body1",
                Deleted = false,
                Image = null
            };


            var mockAnnouncementValidator = new Mock<IAnnouncementValidator>();
            mockAnnouncementValidator.Setup(x => x.PutAnnouncement(1, a)).Returns(a);
            mockAnnouncementValidator.Setup(x => x.secure(null)).Verifiable();
            mockAnnouncementValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new AnnouncementsController(mockAnnouncementValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PutAnnouncement(1, a);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Announcement>>;
            Assert.IsNotNull(actionResult);


        }

        [TestMethod]
        public void TestPostAnnouncement()
        {
            Announcement a = new Announcement()
            {
                AnnouncementId = 1,
                Title = "Announcement1",
                Body = "Body1",
                Deleted = false,
                Image = null
            };

            var mockAnnouncementValidator = new Mock<IAnnouncementValidator>();
            mockAnnouncementValidator.Setup(x => x.PostAnnouncement(a)).Verifiable();
            mockAnnouncementValidator.Setup(x => x.secure(null)).Verifiable();
            mockAnnouncementValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new AnnouncementsController(mockAnnouncementValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PostAnnouncement(a);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Announcement>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestDeleteAnnouncement()
        {
            Announcement a = new Announcement()
            {
                AnnouncementId = 1,
                Title = "Announcement1",
                Body = "Body1",
                Deleted = false,
                Image = null
            };

            var mockAnnouncementValidator = new Mock<IAnnouncementValidator>();
            mockAnnouncementValidator.Setup(x => x.DeleteAnnouncement(1)).Verifiable();
            mockAnnouncementValidator.Setup(x => x.secure(null)).Verifiable();
            mockAnnouncementValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new AnnouncementsController(mockAnnouncementValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteAnnouncement(1);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Announcement>>;
            Assert.IsNotNull(actionResult);


        }
    }
}
