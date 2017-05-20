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
    public class VotesController : ApiController
    {
        private readonly IVoteValidator VoteValidator;      

        public VotesController()
        {
            VoteValidator = SystemFactory.GetVoteValidatorInstance();
        }
        public VotesController(IVoteValidator VoteValidator)
        {
            this.VoteValidator = VoteValidator;
        }


        [ResponseType(typeof(IEnumerable<Vote>))]
        [HttpGet]
        [Route("api/Votes")]
        public IHttpActionResult GetVotes()
        {
            //IEnumerable<string> token;
            //Request.Headers.TryGetValues("TODO_PAGOS_TOKEN", out token);
            //throw new Exception(token.ToString());
            
            try
            {
                VoteValidator.AtmSecure(Request);
                IEnumerable<Vote> Votes = VoteValidator.GetVotes();
                return Ok(Votes);
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
        [Route("api/Votes/{VoteId}")]
        public IHttpActionResult PutVote(int VoteId, [FromBody] Vote Vote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VoteValidator.secure(Request);
                VoteValidator.PutVote(VoteId, Vote);
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

        [ResponseType(typeof(Vote))]
        [Route("api/Votes")]
        [HttpPost]
        public IHttpActionResult PostVote(Vote Vote)
        {
            HttpRequestHeaders HEADERS = Request.Headers;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VoteValidator.PostVote(Vote);
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
            return Ok(Vote);
        }

        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/Votes/logicDelete/{VoteId}")]
        public IHttpActionResult DeleteVote(int VoteId)
        {
            try
            {
                VoteValidator.secure(Request);
                VoteValidator.DeleteVote(VoteId);
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