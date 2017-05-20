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
    public class ContactController : ApiController
    {
        private readonly IContactValidator contactValidator;

        public ContactController()
        {
            contactValidator = SystemFactory.GetContactValidatorInstance();
        }
        public ContactController(IContactValidator contactValidator)
        {
            this.contactValidator = contactValidator;
        }

        // GET: api/Contacts
        [ResponseType(typeof(IEnumerable<Contact>))]
        [HttpGet]
        [Route("api/contacts")]
        public IHttpActionResult GetContacts()
        {
            try
            {
                contactValidator.AtmSecure(Request);
                IEnumerable<Contact> contacts = contactValidator.GetContacts();
                return Ok(contacts);
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

        // GET: api/Contacts/5
        [ResponseType(typeof(Contact))]
        [HttpGet]
        [Route("api/contacts/{id}")]
        public IHttpActionResult GetContact(string id)
        {
            try
            {
                contactValidator.secure(Request);
                Contact contact = contactValidator.GetContact(id);
                return Ok(contact);
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

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/contacts/{contactId}")]
        public IHttpActionResult PutContact(string contactId, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                contactValidator.secure(Request);
                contactValidator.PutContact(contactId, contact);
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

        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        [Route("api/contacts")]
        [HttpPost]
        public IHttpActionResult PostContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //contactValidator.secure(Request);
                contactValidator.PostContact(contact);
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
            return Ok(contact);
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/contacts/logicDelete/{contactId}")]
        public IHttpActionResult DeleteContact(string contactId)
        {
            try
            {
                contactValidator.secure(Request);
                contactValidator.DeleteContact(contactId);
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
                //  contactValidator.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
