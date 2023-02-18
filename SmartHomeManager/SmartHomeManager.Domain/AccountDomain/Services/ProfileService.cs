using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class ProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<int> CreateProfile(Profile profile)
        {
            Profile newProfile = new Profile();
            newProfile.ProfileId = new Guid();
            newProfile.Name = profile.Name;
            newProfile.AccountId = profile.AccountId;
            newProfile.Account = profile.Account;
            /*newProfile.Scenarios = profile.Scenarios;
            newProfile.Devices = profile.Devices;*/

            Debug.WriteLine(profile.Account);

            bool response = await _profileRepository.AddAsync(newProfile);

            if (response)
            {
                int saveResponse = await _profileRepository.SaveAsync();

                if (saveResponse > 0)
                {
                    return 1;
                }
            }

            return 2;
        }

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            IEnumerable<Profile> profiles = await _profileRepository.GetAllAsync();

            if (profiles == null)
            {
                return Enumerable.Empty<Profile>();
            }

            return profiles;
        }

        public async Task<Profile?> GetProfileByProfileId(Guid id)
        {
            Profile? profile = await _profileRepository.GetByIdAsync(id);

            if (profile == null)
            {
                return null;    
            }

            return profile;
            
        }

        public async Task<IEnumerable<Profile?>?> GetProfilesByAccountId(Guid id)
        {
            IEnumerable<Profile> profiles = await _profileRepository.GetProfilesByAccountId(id);
            if (!profiles.Any())
            {
                return null;
            }

            return profiles;
        }

        public async Task<IEnumerable<Guid>?> GetDevicesByProfileId(Guid id)
        {

            IEnumerable<Guid> listOfDeviceIds = await _profileRepository.GetDevicesByProfileId(id);
            if (!listOfDeviceIds.Any())
            {
                return null;
            }

            return listOfDeviceIds;
        }

        public async Task<bool> UpdateProfile(Profile profile)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteProfile(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
