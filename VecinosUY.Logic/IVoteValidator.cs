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
    public interface IVoteValidator:IDisposable
    {
        
        IEnumerable<Vote> GetVotes();

        Vote GetVotesById(int VoteUserId);
        IEnumerable<Vote> GetVotes(string VoteUserId);
        Vote PutVote(int id, Vote Vote);
        void PostVote(Vote Vote);
        void DeleteVote(int VoteId);

        void secure(HttpRequestMessage request);

        void AtmSecure(HttpRequestMessage request);


    }
}
