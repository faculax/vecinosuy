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
    public class ServicesController : ApiController
    {
        private readonly IServiceValidator serviceValidator;

        public ServicesController()
        {
            serviceValidator = SystemFactory.GetServiceValidatorInstance();
        }
        public ServicesController(IServiceValidator serviceValidator)
        {
            this.serviceValidator = serviceValidator;
        }

        // GET: api/Service
        [ResponseType(typeof(IEnumerable<Service>))]
        [HttpGet]
        [Route("api/services")]
        public IHttpActionResult GetServices()
        {
            try
            {
                serviceValidator.AtmSecure(Request);
                IEnumerable<Service> services = serviceValidator.GetServices();
                return Ok(services);
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

        // GET: api/Services/5
        [ResponseType(typeof(Service))]
        [HttpGet]
        [Route("api/services/{id}")]
        public IHttpActionResult GetService(string id)
        {
            try
            {
                serviceValidator.secure(Request);
                Service service = serviceValidator.GetService(id);
                return Ok(service);
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

        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/services/{serviceId}")]
        public IHttpActionResult PutService(string serviceId, [FromBody] Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                serviceValidator.secure(Request);
                serviceValidator.PutService(serviceId, service);
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

        // POST: api/Services
        [ResponseType(typeof(Service))]
        [Route("api/services")]
        [HttpPost]
        public IHttpActionResult PostService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //serviceValidator.secure(Request);
                serviceValidator.PostService(service);
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
            return Ok(service);
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/services/logicDelete/{serviceId}")]
        public IHttpActionResult DeleteService(string serviceId)
        {
            try
            {
                serviceValidator.secure(Request);
                serviceValidator.DeleteService(serviceId);
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
                //  serviceValidator.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
