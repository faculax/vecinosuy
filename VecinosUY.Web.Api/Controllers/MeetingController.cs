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
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace VecinosUY.Web.Api.Controllers
{    
    public class MeetingsController : ApiController
    {
        private readonly IMeetingValidator MeetingValidator;      

        public MeetingsController()
        {
            MeetingValidator = SystemFactory.GetMeetingValidatorInstance();
        }
        public MeetingsController(IMeetingValidator MeetingValidator)
        {
            this.MeetingValidator = MeetingValidator;
        }


        [ResponseType(typeof(IEnumerable<Meeting>))]
        [HttpGet]
        [Route("api/Meetings")]
        public IHttpActionResult GetMeetings()
        {
            //IEnumerable<string> token;
            //Request.Headers.TryGetValues("TODO_PAGOS_TOKEN", out token);
            //throw new Exception(token.ToString());
            
            try
            {
                MeetingValidator.AtmSecure(Request);
                IEnumerable<Meeting> Meetings = MeetingValidator.GetMeetings();
                return Ok(Meetings);
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


        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/Meetings/{MeetingId}")]
        public IHttpActionResult PutMeeting(int MeetingId, [FromBody] Meeting Meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MeetingValidator.secure(Request);
                MeetingValidator.PutMeeting(MeetingId, Meeting);
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

        [ResponseType(typeof(Meeting))]
        [Route("api/Meetings")]
        [HttpPost]
        public IHttpActionResult PostMeeting(Meeting Meeting)
        {
            HttpRequestHeaders HEADERS = Request.Headers;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MeetingValidator.PostMeeting(Meeting);
                AnnouncementsController.notifyAndroidUsers("", false);
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
            return Ok(Meeting);
        }

        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/Meetings/logicDelete/{MeetingId}")]
        public IHttpActionResult DeleteMeeting(int MeetingId)
        {
            try
            {
                MeetingValidator.secure(Request);
                MeetingValidator.DeleteMeeting(MeetingId);
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