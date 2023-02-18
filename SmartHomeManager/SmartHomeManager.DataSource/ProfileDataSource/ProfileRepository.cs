using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.DataSource.ProfileDataSource
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Profile profile)
        {
            await _dbContext.Profiles.AddAsync(profile);

            return true;
        } 

        public async Task<bool> DeleteAsync(Profile profile)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            return await _dbContext.Profiles.ToListAsync();
        }

        public async Task<Profile?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Profiles.FindAsync(id);
        }

        public async Task<int> SaveAsync()
        {
            int result = await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<bool> UpdateAsync(Profile profile)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Profile>> GetProfilesByAccountId(Guid accountId)
        {
            List<Profile> profiles = (await _dbContext.Profiles.ToListAsync())
                .Where(p => p.AccountId == accountId).Select(p => p).ToList();

            if (profiles.Count >= 0)
                return profiles;

            return Enumerable.Empty<Profile>();
        }

        public async Task<IEnumerable<Guid>> GetDevicesByProfileId(Guid profileId)
        {
            List<Guid> deviceGuids = (await _dbContext.DeviceProfiles.ToListAsync())
                .Where(p => p.ProfileId == profileId).Select(p => p.DeviceId).ToList();

            if (deviceGuids.Count >= 0)
                return deviceGuids;

            return Enumerable.Empty<Guid>();
        }
    }
}
