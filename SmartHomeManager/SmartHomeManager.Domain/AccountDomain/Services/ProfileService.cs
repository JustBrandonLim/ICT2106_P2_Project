using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Factory;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<int> CreateProfile(ProfileWebRequest profileWebRequest)
        {
            //Insert profileWebRequest into Factory to create profile

            var profileFactory = ProfileFactory.makeProfile(profileWebRequest);

            bool response = await _profileRepository.AddAsync(profileFactory);

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

        public async Task<bool> UpdateProfile(Profile profile, UpdateProfileWebRequest updateProfileWebRequest)
        {
            profile.Name = updateProfileWebRequest.Name;
            profile.Description = updateProfileWebRequest.Description;
            profile.Pin = updateProfileWebRequest.Pin;


            int updateResponse = await _profileRepository.UpdateAsync(profile);
            if (updateResponse == 1)
            {
                return true;
            }

            return false;
            /* throw new NotImplementedException();*/
        }

        public async Task<bool> DeleteProfile(Profile profile)
        {
            bool deleteResponse = _profileRepository.Delete(profile);
            if (deleteResponse)
            {
                await _profileRepository.SaveAsync();
                return true;
            }

            return false;
            /*throw new NotImplementedException();*/
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

                if (profile.Pin == UserPin) {
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
