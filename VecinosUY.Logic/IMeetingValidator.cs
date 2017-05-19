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
    public interface IMeetingValidator:IDisposable
    {
        
        IEnumerable<Meeting> GetMeetings();

        Meeting GetMeetingsById(int MeetingUserId);
        IEnumerable<Meeting> GetMeetings(string MeetingUserId);
        Meeting PutMeeting(int id, Meeting Meeting);
        void PostMeeting(Meeting Meeting);
        void DeleteMeeting(int MeetingId);

        void secure(HttpRequestMessage request);

        void AtmSecure(HttpRequestMessage request);


    }
}
