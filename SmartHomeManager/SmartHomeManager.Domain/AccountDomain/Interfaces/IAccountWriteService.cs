using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IAccountWriteService
    {
        public Task<int> CreateAccount(AccountWebRequest accountWebRequest);
        public Task<bool> UpdatePassword(PasswordWebRequest passwordWebRequest);
        public Task<bool> UpdateTwoFactorFlag(Guid accountId, bool twoFactorFlag);
        public Task<bool> UpdateAccount(Account account, AccountWebRequest accountWebRequest);
        public Task<bool> DeleteAccount(Account account);
    }
}
