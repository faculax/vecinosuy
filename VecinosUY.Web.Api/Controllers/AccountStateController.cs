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
    public class AccountStatesController : ApiController
    {
        private readonly IAccountStateValidator AccountStateValidator;      

        public AccountStatesController()
        {
            AccountStateValidator = SystemFactory.GetAccountStateValidatorInstance();
        }
        public AccountStatesController(IAccountStateValidator AccountStateValidator)
        {
            this.AccountStateValidator = AccountStateValidator;
        }


        [ResponseType(typeof(IEnumerable<AccountState>))]
        [HttpGet]
        [Route("api/AccountStates")]
        public IHttpActionResult GetAccountStates()
        {
            //IEnumerable<string> token;
            //Request.Headers.TryGetValues("TODO_PAGOS_TOKEN", out token);
            //throw new Exception(token.ToString());
            
            try
            {
                AccountStateValidator.AtmSecure(Request);
                IEnumerable<AccountState> AccountStates = AccountStateValidator.GetAccountStates();
                return Ok(AccountStates);
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
        [ResponseType(typeof(IEnumerable<AccountState>))]
        [HttpGet]
        [Route("api/AccountStates/byId")]
        public IHttpActionResult GetAccountState()
        {
            try
            {
                AccountStateValidator.AtmSecure(Request);
                IEnumerable<string> token;
                Request.Headers.TryGetValues("TODO_PAGOS_TOKEN", out token);
                IEnumerable<AccountState> AccountState = AccountStateValidator.GetAccountStatesById(token.FirstOrDefault());
                return Ok(AccountState);
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
        [Route("api/AccountStates/{AccountStateId}")]
        public IHttpActionResult PutAccountState(int AccountStateId, [FromBody] AccountState AccountState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AccountStateValidator.secure(Request);
                AccountStateValidator.PutAccountState(AccountStateId, AccountState);
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

        [ResponseType(typeof(AccountState))]
        [Route("api/AccountStates")]
        [HttpPost]
        public IHttpActionResult PostAccountState(AccountState AccountState)
        {
            HttpRequestHeaders HEADERS = Request.Headers;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AccountStateValidator.PostAccountState(AccountState);
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
            return Ok(AccountState);
        }

        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/AccountStates/{AccountStateUserId}/logicDelete/{month}/{year}")]
        public IHttpActionResult DeleteAccountState(string AccountStateUserId, int month, int year)
        {
            try
            {
                AccountStateValidator.secure(Request);
                AccountStateValidator.DeleteAccountState(AccountStateUserId,month,year);
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