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
    public interface IAccountStateValidator:IDisposable
    {
        
        IEnumerable<AccountState> GetAccountStates();

        IEnumerable<AccountState> GetAccountStatesById(string AccountStateUserId);
        AccountState GetAccountState(string AccountStateUserId, int month, int year);
        AccountState PutAccountState(int id, AccountState AccountState);
        void PostAccountState(AccountState AccountState);
        void DeleteAccountState(string AccountStateUserId, int month, int year);

        void secure(HttpRequestMessage request);

        void AtmSecure(HttpRequestMessage request);


    }
}
