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
    public class ServiceValidator : IServiceValidator
    {
        private readonly IUnitOfWork unitOfWork;
        public ServiceValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Service> GetServices()
        {
            return unitOfWork.ServiceRepository.Get();
        }

        public Service GetService(string id)
        {
            Service service = null;
            service = unitOfWork.ServiceRepository.GetByID(id);
            if (service == null)
            {
                throw new NotExistException("El servicio especificado no existe");
            }
            return service;
        }

        public Service PutService(string serviceId, Service service)
        {
            service.ServiceId = serviceId;
            Service oldService = GetService(serviceId);
            if (oldService != null)
            {
                oldService.Name = service.Name;
                oldService.Building = service.Building;
                unitOfWork.ServiceRepository.Update(oldService);
                unitOfWork.Save();
            }
            else
            {
                throw new NotExistException("El servicio especificado no existe");
            }
            return service;
        }

        public void PostService(Service service)
        {
            unitOfWork.ServiceRepository.Insert(service);
            unitOfWork.Save();
        }


        public void DeleteService(string serviceId)
        {
            Service service = GetService(serviceId);
            if (service != null)
            {
                unitOfWork.ServiceRepository.Update(service);
                unitOfWork.Save();
                //this.PutService(serviceId, service);
            }
            else
            {
                throw new NotExistException("El servicio especificado no existe");
            }

        }

        private bool ServiceExists(int id)
        {
            return unitOfWork.ServiceRepository.GetByID(id) != null; ;
        }

        public void Dispose()
        {
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
