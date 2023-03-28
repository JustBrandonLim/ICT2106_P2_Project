using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IAccountInfoService
    {
        public Task<Account?> GetAccountByAccountId(Guid id);
    }
}
