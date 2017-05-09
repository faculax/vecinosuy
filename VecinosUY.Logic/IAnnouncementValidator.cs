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
    public interface IAnnouncementValidator:IDisposable
    {
        
        IEnumerable<Announcement> GetAnnouncements();
        Announcement GetAnnouncement(int id);
        Announcement PutAnnouncement(int id, Announcement announcement);
        void PostAnnouncement(Announcement announcement);
        void DeleteAnnouncement(int announcement);

        void secure(HttpRequestMessage request);

        void AtmSecure(HttpRequestMessage request);


    }
}
