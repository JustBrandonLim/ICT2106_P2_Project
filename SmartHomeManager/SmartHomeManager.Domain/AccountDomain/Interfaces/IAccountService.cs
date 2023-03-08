using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IAccountService
    {
        public Task<int> CreateAccount(AccountWebRequest accountWebRequest);
        public Task<Account?> GetAccountByAccountId(Guid id);
        public Task<bool?> GetTwoFactorFlag(Guid id);
        public Task<IEnumerable<Account>> GetAccounts();
        public Task<LoginResponse?> VerifyLogin(LoginWebRequest login);
        /*public Task<bool> CheckAccountExists(Guid id);*/
        public Task<bool> UpdateAccount(Account account, AccountWebRequest accountWebRequest);
        public Task<bool> DeleteAccount(Account account);
        public string GetHashedPassword(Guid accountId, string unhashedPassword);
    }
}
