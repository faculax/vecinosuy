using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using VecinosUY.Data.Entities;
using VecinosUY.Data.Repository;
using VecinosUY.Exceptions;
using VecinosUY.Factory;
using VecinosUY.Logic;

namespace VecinosUY.Web.Api.Controllers
{
    public class BookingsController : ApiController
    {
        private readonly IBookingValidator bookingValidator;

        public BookingsController()
        {
            bookingValidator = SystemFactory.GetBookingValidatorInstance();
        }
        public BookingsController(IBookingValidator bookingValidator)
        {
            this.bookingValidator = bookingValidator;
        }

        // GET: api/Bookings
        [ResponseType(typeof(IEnumerable<Booking>))]
        [HttpGet]
        [Route("api/bookings")]
        public IHttpActionResult GetBookings()
        {
            try
            {
                bookingValidator.AtmSecure(Request);
                IEnumerable<Booking> bookings = bookingValidator.GetBookings();
                return Ok(bookings);
            }
            catch (NotExistException exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Mymessage));
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "VecinosUY no se puede conectar a la base de datos (∩︵∩)"));
            }
            catch (Exception exception)
            {
                //throw new Exception(exception.Message)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message));
            }

        }

        // GET: api/Bookings/5
        [ResponseType(typeof(Booking))]
        [HttpGet]
        [Route("api/bookings/{id}")]
        public IHttpActionResult GetBooking(string id)
        {
            try
            {
                bookingValidator.secure(Request);
                Booking booking = bookingValidator.GetBooking(id);
                return Ok(booking);
            }
            catch (NotExistException exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Mymessage));
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "VecinosUY no se puede conectar a la base de datos (∩︵∩)"));
            }
            catch (Exception exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message));
            }

        }


        // POST: api/Bookings
        [ResponseType(typeof(Booking))]
        [Route("api/bookings")]
        [HttpPost]
        public IHttpActionResult PostBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //bookingValidator.secure(Request);
                bookingValidator.PostBooking(booking);
            }
            catch (NotAdminException exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Mymessage));
            }
            catch (NotExistException exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Mymessage));
            }
            catch (NotValidBookingException exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Mymessage));
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "VecinosUY no se puede conectar a la base de datos (∩︵∩)"));
            }
            catch (Exception exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message));
            }
            return Ok(booking);
        }

        // DELETE: api/Bookings/5
        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/bookings/logicDelete/{bookingId}")]
        public IHttpActionResult DeleteBooking(string bookingId)
        {
            try
            {
                bookingValidator.secure(Request);
                bookingValidator.DeleteBooking(bookingId);
            }
            catch (NotAdminException exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Mymessage));
            }
            catch (NotExistException exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Mymessage));
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "VecinosUY no se puede conectar a la base de datos (∩︵∩)"));
            }
            catch (Exception exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message));
            }
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "OK"));
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //  bookingValidator.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
