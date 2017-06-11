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
    public class BookingControllerTest
    {
        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetBookings()
        {
            var allBookings = new[]
            {
                new Booking()
                {
                    BookingId = 1,
                    User = "luda@gmail.com",
                    Service = "Service1",
                    BookedFrom = DateTime.Now,
                    BookedTo = DateTime.Now.AddDays(1),
                    Deleted = false
                },
               new Booking()
                {
                    BookingId = 2,
                    User = "fran@gmail.com",
                    Service = "Service2",
                    BookedFrom = DateTime.Now,
                    BookedTo = DateTime.Now.AddDays(1),
                    Deleted = false
                },
            };

            var mockBookingValidator = new Mock<IBookingValidator>();
            mockBookingValidator.Setup(x => x.GetBookings()).Returns(allBookings);

            var controller = new BookingsController(mockBookingValidator.Object);

            // Arrange            
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");

            // Don't forget these lines, if you do then the request will be null.
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetBookings();
            //throw new Exception(actionResult.GetType().ToString());
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Booking>>;

            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception))]
        public void TestGetBooking()
        {
            new Booking()
            {
                BookingId = 1,
                User = "luda@gmail.com",
                Service = "Service1",
                BookedFrom = DateTime.Now,
                BookedTo = DateTime.Now.AddDays(1),
                Deleted = false
            };

            var mockBookingValidator = new Mock<IBookingValidator>();

            var controller = new BookingsController(mockBookingValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.GetBooking("1");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Booking>>;
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void TestPostBooking()
        {
            Booking a = new Booking()
            {
                BookingId = 1,
                User = "luda@gmail.com",
                Service = "Service1",
                BookedFrom = DateTime.Now,
                BookedTo = DateTime.Now.AddDays(1),
                Deleted = false
            };

            var mockBookingValidator = new Mock<IBookingValidator>();
            mockBookingValidator.Setup(x => x.PostBooking(a)).Verifiable();
            mockBookingValidator.Setup(x => x.secure(null)).Verifiable();
            mockBookingValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new BookingsController(mockBookingValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.PostBooking(a);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Booking>>;
            Assert.IsNotNull(actionResult);


        }
        [TestMethod]
        public void TestDeleteBooking()
        {
            Booking a = new Booking()
            {
                BookingId = 1,
                User = "luda@gmail.com",
                Service = "Service1",
                BookedFrom = DateTime.Now,
                BookedTo = DateTime.Now.AddDays(1),
                Deleted = false
            };

            var mockBookingValidator = new Mock<IBookingValidator>();
            mockBookingValidator.Setup(x => x.DeleteBooking("1")).Verifiable();
            mockBookingValidator.Setup(x => x.secure(null)).Verifiable();
            mockBookingValidator.Setup(x => x.AtmSecure(null)).Verifiable();

            var controller = new BookingsController(mockBookingValidator.Object);
            var controllerContext = new HttpControllerContext();
            var request = new HttpRequestMessage();
            request.Headers.Add("TODO_PAGOS_TOKEN", "1");
            controllerContext.Request = request;
            controller.ControllerContext = controllerContext;

            IHttpActionResult actionResult = controller.DeleteBooking("1");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Booking>>;
            Assert.IsNotNull(actionResult);


        }
    }
}
