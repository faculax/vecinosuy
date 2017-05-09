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
    public class AnnouncementValidator:IAnnouncementValidator
    {
        private readonly IUnitOfWork unitOfWork;
        public AnnouncementValidator(IUnitOfWork unitOfWork)
        {            
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Announcement> GetAnnouncements()
        {        
            return unitOfWork.AnnouncementRepository.Get(x => x.Deleted == false);
        }

        public Announcement GetAnnouncement(int id)
        {
            Announcement announcement = null;
            announcement = unitOfWork.AnnouncementRepository.GetByID(id);            
            if (announcement == null)
            {
                throw new NotExistException("El anuncio especificado no existe o usted no esta logueado");
            }
            if (announcement.Deleted)
            {
                throw new NotExistException("El anuncio ha sido borrado");
            }
            return announcement;
        }

        public Announcement PutAnnouncement(int announcementId, Announcement announcement)
        {
            announcement.AnnouncementId = announcementId;
            Announcement oldAnnouncement = GetAnnouncement(announcementId);            
            if (oldAnnouncement != null)
            {
                oldAnnouncement.Title = announcement.Title;
                oldAnnouncement.Deleted = announcement.Deleted;
                oldAnnouncement.Body = announcement.Body;             
                unitOfWork.AnnouncementRepository.Update(oldAnnouncement);
                unitOfWork.Save();                
            }
            else
            {
                throw new NotExistException("El anuncio especificado no existe");
            }            
            return announcement;            
        }

        public void PostAnnouncement(Announcement announcement)
        {
            unitOfWork.AnnouncementRepository.Insert(announcement);
            unitOfWork.Save();
        }


        public void DeleteAnnouncement(int announcementId)
        {
            Announcement announcement = GetAnnouncement(announcementId);
            if (announcement != null)
            {
                announcement.Deleted = true;
                unitOfWork.AnnouncementRepository.Update(announcement);
                unitOfWork.Save();
                //this.PutAnnouncement(announcementId, announcement);
            }
            else
            {
                throw new NotExistException("El anuncio especificado no existe");
            }
            
        }


        private bool AnnouncementExists(int id)
        {            
            return unitOfWork.AnnouncementRepository.GetByID(id) != null; ;
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
