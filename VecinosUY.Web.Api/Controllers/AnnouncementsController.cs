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
        [Route("api/announcements/admin")]
        [HttpPost]
        public IHttpActionResult PostAnnouncementAdmin(Announcement announcement)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                announcementValidator.PostAnnouncement(announcement);
                notifyAndroidUsers(announcement.Title);

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
            return Ok();
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




        private void notifyAndroidUsers(string title)
        {
            try
            {

                HttpClient client = new HttpClient();
                string RestUrl = "https://fcm.googleapis.com/fcm/send";
                jsonNotification jsn = new jsonNotification();
                jsn.body = title;
                jsn.title = "El admin anuncio:";
                jsonFirebaseRequest jfr = new jsonFirebaseRequest();
                jfr.to = "/topics/allDevices";
                jfr.notification = jsn;
                var json = JsonConvert.SerializeObject(jfr);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=AAAA8jvJGC0:APA91bGtrLm7bF9IQtK4Uhoh6eFn9sjfRk24bWSAJDiUjGgtwYzJBX4rnu_w-0HKDgaMfMuA5103GejnOCLXx1rwqsipmHP18SmeumQ5jrNISXEDydwY-Ef_m9pgM7O-AHlf1mSn4CdZ");
                content.Headers.TryAddWithoutValidation("Authorization", "key=AAAA8jvJGC0:APA91bGtrLm7bF9IQtK4Uhoh6eFn9sjfRk24bWSAJDiUjGgtwYzJBX4rnu_w-0HKDgaMfMuA5103GejnOCLXx1rwqsipmHP18SmeumQ5jrNISXEDydwY-Ef_m9pgM7O-AHlf1mSn4CdZ");

                HttpResponseMessage response = null;

                //  response = await client.PostAsync(RestUrl, content);
                client.PostAsync(RestUrl, content);

              //  if (response.IsSuccessStatusCode)
              //  {
                    //Debug.WriteLine("Envío de Remito: Se envió correctamente");
                //}
                //else
                //{
                    //UserDialogs.Instance.InfoToast("Error al enviar remito");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            public class jsonFirebaseRequest {
            public string to { get; set; }
            public jsonNotification notification { get; set; }

        }

        public class jsonNotification {
            public string title { get; set; }
            public string body { get; set; }
        }
        }


}