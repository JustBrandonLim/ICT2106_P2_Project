using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.DataSource.AccountDataSource
{
    public class AccountSeedData
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // If there is data, don't do anything
            if (context.Accounts.Any()) return;

            // create objects
            var accounts = new List<Account>
            {
                new Account()
                {
                    AccountId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Email = "abc@xyz.com",
                    Username = "username",
                    Password = "P@ssw0rd",
                    Address = "123 abc 456 888888",
                    Timezone = 8,
                    TwoFactorFlag = true
                }
            };

            // add to repository and commit those changes
            await context.Accounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();
        }
    }
}
