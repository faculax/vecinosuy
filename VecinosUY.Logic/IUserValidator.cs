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

namespace VecinosUY.Logic
{
    public interface IUserValidator:IDisposable
    {
        
        IEnumerable<User> GetUsers();
        User LogIn(int userId, string pass);
        User GetUser(int id);
        User PutUser(int userId, User user);
        void PostUser(User user);
        void DeleteUser(int userId);

        void secure(HttpRequestMessage request);

        void AtmSecure(HttpRequestMessage request);


    }
}
