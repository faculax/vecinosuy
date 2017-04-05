﻿using System.Collections.Generic;
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

namespace VecinosUY.Web.Api.Controllers
{    
    public class UsersController : ApiController
    {
        private readonly IUserValidator userValidator;      

        public UsersController()
        {
            userValidator = SystemFactory.GetUserValidatorInstance();
        }
        public UsersController(IUserValidator userValidator)
        {
            this.userValidator = userValidator;
        }
        [ResponseType(typeof(User))]
        [HttpGet]
        [Route("api/users/{userId}/login/{pass}")]
        public IHttpActionResult LogIn(int userId, string pass)
        {
            try
            {
                User user = userValidator.LogIn(userId, pass);
                return Ok(user);
            }
            catch (NotExistException exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Mymessage));
            }
            catch (Exception exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message));
            }
        }

        // GET: api/Users
        [ResponseType(typeof(IEnumerable<UserDTO>))]
        [HttpGet]
        [Route("api/users")]
        public IHttpActionResult GetUsers()
        {
            //IEnumerable<string> token;
            //Request.Headers.TryGetValues("TODO_PAGOS_TOKEN", out token);
            //throw new Exception(token.ToString());
            
            try
            {
                userValidator.AtmSecure(Request);
                IEnumerable<User> users = userValidator.GetUsers();
                List<UserDTO> listUserDTO = new List<UserDTO>();
                IEnumerable<UserDTO> usersDTO;
                foreach (var item in users)
                {
                    UserDTO userDTO = new UserDTO()
                    {
                        UserId = item.UserId,
                        Name = item.Name,
                        Admin = item.Admin,
                        Deleted = item.Deleted
                    };
                    listUserDTO.Add(userDTO);

                }
                usersDTO = listUserDTO.AsEnumerable();
                return Ok(usersDTO);
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
        // GET: api/Users/5
        [ResponseType(typeof(UserDTO))]
        [HttpGet]
        [Route("api/users/{id}")]
        public IHttpActionResult GetUser(int id)
        {
            try
            {
                userValidator.secure(Request);
                User user = userValidator.GetUser(id);
                UserDTO userDTO = new UserDTO()
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Admin = user.Admin,
                    Deleted = user.Deleted
                };
                return Ok(userDTO);
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

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/users/{userId}")]
        public IHttpActionResult PutUser(int userId, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                userValidator.secure(Request);
                userValidator.PutUser(userId, user);
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        [Route("api/users")]
        [HttpPost]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
      //          userValidator.secure(Request);
                userValidator.PostUser(user);
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
            return Ok(user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(void))]
        [HttpDelete]
        [Route("api/users/{userId}")]
        public IHttpActionResult DeleteUser(int userId)
        {
            try
            {
                userValidator.secure(Request);
                userValidator.DeleteUser(userId);
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
              //  userValidator.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}