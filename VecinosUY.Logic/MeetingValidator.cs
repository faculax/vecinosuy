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
    public class MeetingValidator:IMeetingValidator
    {
        private readonly IUnitOfWork unitOfWork;
        public MeetingValidator(IUnitOfWork unitOfWork)
        {            
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Meeting> GetMeetings()
        {        
            return unitOfWork.MeetingRepository.Get(x => x.Deleted == false);
        }

        public Meeting GetMeetingsById(int meetingId)
        {
            Meeting meeting = unitOfWork.MeetingRepository.GetByID(meetingId);

            if (meeting == null)
            {
                throw new NotExistException("Los estados de cuenta especificados no existen o usted no esta logueado");
            }
            return meeting;
        }

        public IEnumerable<Meeting> GetMeetings(string MeetingUserId)
        {
            return unitOfWork.MeetingRepository.Get();
        }

        public Meeting PutMeeting(int MeetingId, Meeting Meeting)
        {
                    
            return Meeting;            
        }

        public void PostMeeting(Meeting Meeting)
        {
            unitOfWork.MeetingRepository.Insert(Meeting);
            unitOfWork.Save();
        }


        public void DeleteMeeting(int MeetingId)
        {
            Meeting Meeting = GetMeetingsById(MeetingId);
            if (Meeting != null)
            {
                Meeting.Deleted = true;
                unitOfWork.MeetingRepository.Update(Meeting);
                unitOfWork.Save();
                //this.PutMeeting(MeetingId, Meeting);
            }
            else
            {
                throw new NotExistException("El estado de cuenta especificado no existe");
            }
            
        }


        private bool MeetingExists(int id)
        {            
            return unitOfWork.MeetingRepository.GetByID(id) != null; ;
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
