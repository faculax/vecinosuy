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
    public class VoteValidator:IVoteValidator
    {
        private readonly IUnitOfWork unitOfWork;
        public VoteValidator(IUnitOfWork unitOfWork)
        {            
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Vote> GetVotes()
        {        
            return unitOfWork.VoteRepository.Get(x => x.Deleted == false);
        }

        public Vote GetVotesById(int VoteId)
        {
            Vote Vote = unitOfWork.VoteRepository.GetByID(VoteId);

            if (Vote == null)
            {
                throw new NotExistException("Los estados de cuenta especificados no existen o usted no esta logueado");
            }
            return Vote;
        }

        public IEnumerable<Vote> GetVotes(string VoteUserId)
        {
            return unitOfWork.VoteRepository.Get();
        }

        public Vote PutVote(int VoteId, Vote Vote)
        {
            Vote.VoteId = VoteId;
            Vote oldVote = GetVotesById(VoteId);
            if (oldVote != null)
            {
                oldVote.Yes = Vote.Yes;
                oldVote.Deleted = Vote.Deleted;
                oldVote.EndDate = Vote.EndDate;
                oldVote.No = Vote.No;
                oldVote.YesNoQuestion = Vote.YesNoQuestion;
                unitOfWork.VoteRepository.Update(oldVote);
                unitOfWork.Save();
            }
            else
            {
                throw new NotExistException("El usuario especificado no existe");
            }
            return Vote;            
        }

        public void PostVote(Vote Vote)
        {
            unitOfWork.VoteRepository.Insert(Vote);
            unitOfWork.Save();
        }


        public void DeleteVote(int VoteId)
        {
            Vote Vote = GetVotesById(VoteId);
            if (Vote != null)
            {
                Vote.Deleted = true;
                unitOfWork.VoteRepository.Update(Vote);
                unitOfWork.Save();
                //this.PutVote(VoteId, Vote);
            }
            else
            {
                throw new NotExistException("El estado de cuenta especificado no existe");
            }
            
        }


        private bool VoteExists(int id)
        {            
            return unitOfWork.VoteRepository.GetByID(id) != null; ;
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
