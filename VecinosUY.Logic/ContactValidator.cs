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
    public class ContactValidator : IContactValidator
    {
        private readonly IUnitOfWork unitOfWork;
        public ContactValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Contact> GetContacts()
        {
            return unitOfWork.ContactRepository.Get();
        }


        public Contact GetContact(string id)
        {
            Contact contact = null;
            contact = unitOfWork.ContactRepository.GetByID(id);
            if (contact == null)
            {
                throw new NotExistException("El contacto especificado no existe");
            }
            return contact;
        }

        public Contact PutContact(string contactId, Contact contact)
        {
            contact.ContactId = contactId;
            Contact oldContact = GetContact(contactId);
            if (oldContact != null)
            {
                oldContact.Name = contact.Name;
                oldContact.Phone = contact.Phone;
                oldContact.Apartment = contact.Apartment;
                unitOfWork.ContactRepository.Update(oldContact);
                unitOfWork.Save();
            }
            else
            {
                throw new NotExistException("El contacto especificado no existe");
            }
            return contact;
        }

        public void PostContact(Contact contact)
        {
            unitOfWork.ContactRepository.Insert(contact);
            unitOfWork.Save();
        }


        public void DeleteContact(string contactId)
        {
            Contact contact = GetContact(contactId);
            if (contact != null)
            {
                unitOfWork.ContactRepository.Delete(contact);
                unitOfWork.Save();
                
            }
            else
            {
                throw new NotExistException("El contactp especificado no existe");
            }

        }


        private bool ContactExists(int id)
        {
            return unitOfWork.ContactRepository.GetByID(id) != null; ;
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
