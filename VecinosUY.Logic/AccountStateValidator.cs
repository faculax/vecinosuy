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
    public class AccountStateValidator:IAccountStateValidator
    {
        private readonly IUnitOfWork unitOfWork;
        public AccountStateValidator(IUnitOfWork unitOfWork)
        {            
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<AccountState> GetAccountStates()
        {        
            return unitOfWork.AccountStateRepository.Get(x => x.Deleted == false);
        }

        public IEnumerable<AccountState> GetAccountStatesById(string AccountStateUserId)
        {
            List<AccountState> AccountState = new List<AccountState>();
            AccountState[] ats = unitOfWork.AccountStateRepository.Get().ToArray();
            for (int i = 0; i < ats.Length; i++)
            {
                if (ats[i].UserId.Equals(AccountStateUserId)
                    )
                {
                    AccountState.Add(ats[i]);
                }
            }
            if (AccountState == null)
            {
                throw new NotExistException("Los estados de cuenta especificados no existen o usted no esta logueado");
            }
            return AccountState;
        }

        public AccountState GetAccountState(string AccountStateUserId, int month, int year)
        {
            AccountState AccountState = null;
            AccountState[] ats = unitOfWork.AccountStateRepository.Get().ToArray();
            for (int i = 0; i < ats.Length; i++) {
                if (ats[i].UserId.Equals(AccountStateUserId)
                    && (ats[i].Month == month)
                    && (ats[i].Year == year)
                    ) {
                    AccountState = ats[i];
                    break;
                }
            }
            if (AccountState == null)
            {
                throw new NotExistException("El estado de cuenta especificado no existe o usted no esta logueado");
            }
            if (AccountState.Deleted)
            {
                throw new NotExistException("El estado de cuenta ha sido borrado");
            }
            return AccountState;
        }

        public AccountState PutAccountState(int AccountStateId, AccountState AccountState)
        {
                    
            return AccountState;            
        }

        public void PostAccountState(AccountState AccountState)
        {
            unitOfWork.AccountStateRepository.Insert(AccountState);
            unitOfWork.Save();
        }


        public void DeleteAccountState(string AccountStateUserId, int month, int year)
        {
            AccountState AccountState = GetAccountState(AccountStateUserId, month, year);
            if (AccountState != null)
            {
                AccountState.Deleted = true;
                unitOfWork.AccountStateRepository.Update(AccountState);
                unitOfWork.Save();
                //this.PutAccountState(AccountStateId, AccountState);
            }
            else
            {
                throw new NotExistException("El estado de cuenta especificado no existe");
            }
            
        }


        private bool AccountStateExists(int id)
        {            
            return unitOfWork.AccountStateRepository.GetByID(id) != null; ;
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
