using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.DataSource.ProfileDataSource
{
    public class ProfileSeedData
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // If there is data, don't do anything
            if (context.Profiles.Any()) return;

            // create objects
            var profiles = new List<Profile>
            {
                new Profile()
                {
                    ProfileId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    AccountId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Default Profile",
                    Description = "Controlling Fans",
                    Pin = 1234
                }
            };

            // add to repository and commit those changes
            await context.Profiles.AddRangeAsync(profiles);
            await context.SaveChangesAsync();
        }
    }
}
