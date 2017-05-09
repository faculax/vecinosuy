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
    public class BuildingsController : ApiController
    {
        private readonly IBuildingValidator buildingValidator;

        public BuildingsController()
        {
            buildingValidator = SystemFactory.GetBuildingValidatorInstance();
        }
        public BuildingsController(IBuildingValidator buildingValidator)
        {
            this.buildingValidator = buildingValidator;
        }

        // GET: api/Buildings
        [ResponseType(typeof(IEnumerable<Building>))]
        [HttpGet]
        [Route("api/buildings")]
        public IHttpActionResult GetBuildings()
        {
            try
            {
                buildingValidator.AtmSecure(Request);
                IEnumerable<Building> buildings = buildingValidator.GetBuildings();
                return Ok(buildings);
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

        // GET: api/Buildings/5
        [ResponseType(typeof(Building))]
        [HttpGet]
        [Route("api/buildings/{id}")]
        public IHttpActionResult GetBuilding(string id)
        {
            try
            {
                buildingValidator.secure(Request);
                Building building = buildingValidator.GetBuilding(id);
                return Ok(building);
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

        // PUT: api/Buildings/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/buildings/{buildingId}")]
        public IHttpActionResult PutBuilding(string buildingId, [FromBody] Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                buildingValidator.secure(Request);
                buildingValidator.PutBuilding(buildingId, building);
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

        // POST: api/Buildings
        [ResponseType(typeof(Building))]
        [Route("api/buildings")]
        [HttpPost]
        public IHttpActionResult PostBuilding(Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //buildingValidator.secure(Request);
                buildingValidator.PostBuilding(building);
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
            return Ok(building);
        }

        // DELETE: api/Buildings/5
        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/buildings/logicDelete/{buildingId}")]
        public IHttpActionResult DeleteBuilding(string buildingId)
        {
            try
            {
                buildingValidator.secure(Request);
                buildingValidator.DeleteBuilding(buildingId);
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
                //  buildingValidator.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
