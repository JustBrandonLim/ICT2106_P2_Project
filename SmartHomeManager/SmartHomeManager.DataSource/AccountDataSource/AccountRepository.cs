using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.DataSource.AccountDataSource
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Account account)
        {
            // create account
            await _dbContext.Accounts.AddAsync(account);
            
            return true;
        }

        public async Task<int> Update(Account account)
        {
            _dbContext.Entry(account).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public bool Delete(Account account)
        {
            _dbContext.Accounts.Remove(account);
            return true;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            // get all accounts
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<Account?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Accounts.FindAsync(id);
        }
        
        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            Account? tempAcc = await _dbContext.Accounts.Where(acc => acc.Email == email).FirstOrDefaultAsync();
            return tempAcc;
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            Account? tempAcc = await _dbContext.Accounts.Where(acc => acc.Email == email).FirstOrDefaultAsync();
            if (tempAcc == null) return true;
            return false;
        }

        public async Task<int> SaveAsync()
        {
            int result = await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
