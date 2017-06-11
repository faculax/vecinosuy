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
    public class MeetingControllerTest
    {
        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetMeetings()
        {
            var allMeetings = new[]
            {
                new Meeting()
                {
                    MeetingId = 1,
                    Date = DateTime.Now.AddDays(5),
                    Subject = "Subject",
                    Deleted = false
                },
               new Meeting()
                {
                    MeetingId = 2,
                    Date = DateTime.Now.AddDays(3),
                    Subject = "Subject",
                    Deleted = false
                },
            };

            var mockMeetingValidator = new Mock<IMeetingValidator>();
            mockMeetingValidator.Setup(x => x.GetMeetings()).Returns(allMeetings);

            var controller = new MeetingsController(mockMeetingValidator.Object);

            // Arrange            
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");

            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetMeetings();
            //throw new Exception(actionResult.GetType().ToString());
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Meeting>>;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void TestPostMeeting()
        {
            Meeting a = new Meeting()
            {
                MeetingId = 1,
                Date = DateTime.Now.AddDays(5),
                Subject = "Subject",
                Deleted = false
            };

            var mockMeetingValidator = new Mock<IMeetingValidator>();
            mockMeetingValidator.Setup(x => x.PostMeeting(a)).Verifiable();
            mockMeetingValidator.Setup(x => x.secure(null)).Verifiable();
            mockMeetingValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new MeetingsController(mockMeetingValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PostMeeting(a);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Meeting>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestDeleteMeeting()
        {
            Meeting a = new Meeting()
            {
                MeetingId = 1,
                Date = DateTime.Now.AddDays(5),
                Subject = "Subject",
                Deleted = false
            };

            var mockMeetingValidator = new Mock<IMeetingValidator>();
            mockMeetingValidator.Setup(x => x.DeleteMeeting(1)).Verifiable();
            mockMeetingValidator.Setup(x => x.secure(null)).Verifiable();
            mockMeetingValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new MeetingsController(mockMeetingValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteMeeting(1);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Meeting>>;
            Assert.IsNotNull(actionResult);


        }
    }
}
