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
    public class PropertiesValidator:IPropertiesValidator
    {
        private readonly IUnitOfWork unitOfWork;
        public PropertiesValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }       

        public IEnumerable<Property> GetProperties()
        {
            return unitOfWork.PropertyRepository.Get();                
        }

        public Property GetProperty(string id)
        {
            Property property = null;
            foreach (Property prop in unitOfWork.PropertyRepository.Get()) {
                if (prop.PropertyKey.Equals(id)) {
                    property = prop;
                }
            }
            if (property == null) 
            {
                throw new NotExistException("La propiedad especificada no existe");
            }
            return property;
        }

        public void PostProperty(Property property)
        {
            unitOfWork.PropertyRepository.Insert(property);
            unitOfWork.Save();            
        }

        public void PutProperty(int propId, Property property)
        {
            property.PropertyId = propId;
            Property oldProp = GetProperty(property.PropertyKey);
            if (oldProp != null)
            {
                oldProp.Value = property.Value;
                unitOfWork.PropertyRepository.Update(oldProp);
                unitOfWork.Save();
            }
            else
            {
                throw new NotExistException("La propiedad especificado no existe");
            }
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