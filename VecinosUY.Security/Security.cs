using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using VecinosUY.Exceptions;
using VecinosUY.Data.DataAccess;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using VecinosUY.Data.Entities;

namespace VecinosUY.Security
{
    public class Security
    {
        public static string logedUserId { get; set; }
        public static void secure(HttpRequestMessage request) {
            User usr = getUserLoged(request);
            if (!usr.Admin)
            {
                throw new NotAdminException("El usuario no tiene permiso para realizar esta acción");
            }
            logedUserId = usr.UserId;
        }

        public static void AtmSecure(HttpRequestMessage request)
        {
            User usr = getUserLoged(request);
            logedUserId = usr.UserId;
        }

        private static User getUserLoged(HttpRequestMessage request)
        {
            VecinosUYContext db = new VecinosUYContext();
            IEnumerable<string> token;
            request.Headers.TryGetValues("TODO_PAGOS_TOKEN", out token);
            if (token == null)
            {
                throw new NotExistException("no logueado");
            }
            User usr = db.Users.Find(token.FirstOrDefault());
            if (usr == null)
            {
                throw new NotExistException("tocken incorrecto");
            }
            return usr;
        }
    }
}
