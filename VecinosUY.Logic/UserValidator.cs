using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VecinosUY.Data.Entities;
using VecinosUY.Data.DataAccess;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using VecinosUY.Exceptions;
using VecinosUY.Data.Repository;
using System.Net.Http;
using VecinosUY.Logger;

namespace VecinosUY.Logic
{
    public class UserValidator:IUserValidator
    {
        private readonly IUnitOfWork unitOfWork;
        public UserValidator(IUnitOfWork unitOfWork)
        {            
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<User> GetUsers()
        {        
            return unitOfWork.UserRepository.Get(x => x.Deleted == false);
        }


        public User LogIn(string userId, string pass)
        {            
            User user = unitOfWork.UserRepository.GetByID(userId);
            if (user != null) {
                if (user.Password.Equals(pass))
                {
                    unitOfWork.Logger.logg("LOGIN", DateTime.Now, userId + "");
                    user.Token = "1234";
                    this.PutUser(userId, user);
                    return user;
                }                                            
            }            
            throw new NotExistException("El usuario especificado no existe o su contraseña es incorrecta");
        }

        public User ValidateToken(string userId, string token)
        {
            User user = unitOfWork.UserRepository.GetByID(userId);
            if (user != null)
            {
                if (user.Token.Equals(token))
                {
                    //  unitOfWork.Logger.logg("LOGIN", DateTime.Now, userId + "");
                    return user;
                }
            }
            throw new NotExistException("El usuario especificado no tiene el token correcto, contacte al admin");
        }


        public User GetUser(string id)
        {
            User user = null;
            user = unitOfWork.UserRepository.GetByID(id);            
            if (user == null)
            {
                throw new NotExistException("El usuario especificado no existe o usted no esta logueado");
            }
            if (user.Deleted)
            {
                throw new NotExistException("El usuario ha sido borrado");
            }
            return user;
        }

        public User PutUser(string userId, User user)
        {
            user.UserId = userId;         
            User oldUser = GetUser(userId);            
            if (oldUser != null)
            {
                oldUser.Admin = user.Admin;
                oldUser.Deleted = user.Deleted;
                oldUser.Name = user.Name;
                oldUser.Token = user.Token;
                oldUser.Phone = user.Phone;
                if (user.Password != "*****")
                {
                    oldUser.Password = user.Password;
                }                
                unitOfWork.UserRepository.Update(oldUser);
                unitOfWork.Save();                
            }
            else
            {
                throw new NotExistException("El usuario especificado no existe");
            }            
            return user;            
        }

        public void PostUser(User user)
        {
            unitOfWork.UserRepository.Insert(user);
            unitOfWork.Save();

            //add user as contact
            Contact contact = new Contact();
            contact.ContactId = user.UserId;
            contact.Name = user.Name;
            contact.Phone = user.Phone;
            contact.Apartment = "N/A";
            unitOfWork.ContactRepository.Insert(contact);
            unitOfWork.Save();
        }


        public void DeleteUser(string userId)
        {
            User user = GetUser(userId);
            if (user != null)
            {
                user.Deleted = true;
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
                //this.PutUser(userId, user);
            }
            else
            {
                throw new NotExistException("El usuario especificado no existe");
            }
            
        }

        public void DeleteToken(string userId)
        {
            User user = GetUser(userId);
            if (user != null)
            {
                user.Token = "";
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
                //this.PutUser(userId, user);
            }
            else
            {
                throw new NotExistException("El usuario especificado no existe");
            }

        }

        private bool UserExists(int id)
        {            
            return unitOfWork.UserRepository.GetByID(id) != null; ;
        }

        public void Dispose() {
            unitOfWork.Dispose();
        }

        public void secure(HttpRequestMessage request)
        {
            Security.Security.secure(request);
        }

        public void AtmSecure(HttpRequestMessage request)
        {
            Security.Security.AtmSecure(request);
        }
    }
}
