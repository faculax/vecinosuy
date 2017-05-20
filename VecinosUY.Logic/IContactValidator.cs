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
    public interface IContactValidator : IDisposable
    {

        IEnumerable<Contact> GetContacts();
        Contact GetContact(string id);
        Contact PutContact(string contactId, Contact contact);
        void PostContact(Contact contact);
        void DeleteContact(string ContactId);

        void secure(HttpRequestMessage request);

        void AtmSecure(HttpRequestMessage request);
    }
}
