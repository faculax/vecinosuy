using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using VecinosUY.Data.Entities;
using VecinosUY.Logic;
using VecinosUY.Exceptions;
using System.Net.Http;
using System.Net;
using System;
using VecinosUY.Data.Repository;
using VecinosUY.Factory;
using System.Net.Http.Headers;

namespace VecinosUY.Web.Api.Controllers
{    
    public class AnnouncementsController : ApiController
    {
        private readonly IAnnouncementValidator announcementValidator;      

        public AnnouncementsController()
        {
            announcementValidator = SystemFactory.GetAnnouncementValidatorInstance();
        }
        public AnnouncementsController(IAnnouncementValidator announcementValidator)
        {
            this.announcementValidator = announcementValidator;
        }


        [ResponseType(typeof(IEnumerable<Announcement>))]
        [HttpGet]
        [Route("api/announcements")]
        public IHttpActionResult GetAnnouncements()
        {
            //IEnumerable<string> token;
            //Request.Headers.TryGetValues("TODO_PAGOS_TOKEN", out token);
            //throw new Exception(token.ToString());
            
            try
            {
                announcementValidator.AtmSecure(Request);
                IEnumerable<Announcement> announcements = announcementValidator.GetAnnouncements();
                return Ok(announcements);
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
                //throw new Exception(exception.Message);Luis
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message));
            }
            
        }
        [ResponseType(typeof(Announcement))]
        [HttpGet]
        [Route("api/announcements/{id}")]
        public IHttpActionResult GetAnnouncement(int id)
        {
            try
            {
                announcementValidator.secure(Request);
                Announcement announcement = announcementValidator.GetAnnouncement(id);
                return Ok(announcement);
            }
            catch (NotExistException exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Mymessage));
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "VecinosUY no se puede conectar a la base de datos (∩︵∩)"));
            }
            catch (Exception exception) {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message));
            }
            
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/announcements/{announcementId}")]
        public IHttpActionResult PutAnnouncement(int announcementId, [FromBody] Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                announcementValidator.secure(Request);
                announcementValidator.PutAnnouncement(announcementId, announcement);
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

        [ResponseType(typeof(Announcement))]
        [Route("api/announcements")]
        [HttpPost]
        public IHttpActionResult PostAnnouncement(Announcement announcement)
        {
            HttpRequestHeaders HEADERS = Request.Headers;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                announcementValidator.PostAnnouncement(announcement);
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
            return Ok(announcement);
        }

        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/announcements/logicDelete/{announcementId}")]
        public IHttpActionResult DeleteAnnouncement(int announcementId)
        {
            try
            {
                announcementValidator.secure(Request);
                announcementValidator.DeleteAnnouncement(announcementId);
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
            }
            base.Dispose(disposing);
        }



    }


}