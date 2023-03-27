using System;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using System.Diagnostics;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
	public class ProfileReadService : IProfileReadService
	{
        private readonly IProfileRepository _profileRepository;

        public ProfileReadService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            IEnumerable<Profile> profiles = await _profileRepository.GetAllAsync();

            if (!profiles.Any())
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

        public async Task<int> CheckPinByProfileId(ParentControlPin PinInfo)
        {
            // Get the profile with the profileId
            Profile? profile = await _profileRepository.GetByIdAsync(PinInfo.ProfileId);
            int? UserPin = PinInfo.Pin; // user keyed in 
            Debug.WriteLine(" profile is " + profile);

            if (profile.Pin == null)
            {
                Debug.WriteLine("Adult profile ");
                return 3;    // it's an adult
            }

            if (UserPin >= 0 && UserPin <= 9999)    // child profile since theres pin 
            {
                // restrict access to Device page and Account settings page, prompt to key in pin to access the pages
                /*Debug.WriteLine("Child profile: " + profile.Pin)*/

                if (profile.Pin == UserPin)
                {
                    return 1;    // child profile with correct pin
                }
                return 2;    // child profile with wrong pin
            }
            return 4;
        }

        public async Task<int> CheckAdultProfile(ProfileIdRequest ProfileIdInfo)
        {
            Profile? profile = await _profileRepository.GetByIdAsync(ProfileIdInfo.ProfileId);
            if (profile.Pin == null)
            {
                return 1;
            }
            return 2;
        }
    }
}

