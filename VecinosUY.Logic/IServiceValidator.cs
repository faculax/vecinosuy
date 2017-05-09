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
    public interface IServiceValidator : IDisposable
    {
        IEnumerable<Service> GetServices();
        Service GetService(string id);
        Service PutService(string serviceId, Service service);
        void PostService(Service service);
        void DeleteService(string ServiceId);

        void secure(HttpRequestMessage request);

        void AtmSecure(HttpRequestMessage request);
    }
}
