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
using VecinosUY.Security;
using VecinosUY.Data.Repository;
using System.Net.Http;

namespace VecinosUY.Logic
{
    public interface IPropertiesValidator
    {

        IEnumerable<Property> GetProperties();

       Property GetProperty(string id);

        void PostProperty(Property prop);
        void PutProperty(int propId, Property property);

        void Dispose();

        void secure(HttpRequestMessage request);

        void AtmSecure(HttpRequestMessage request);


    }
}
