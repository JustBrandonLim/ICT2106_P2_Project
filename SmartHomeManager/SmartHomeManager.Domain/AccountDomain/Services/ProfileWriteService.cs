using System;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Factory;
using SmartHomeManager.Domain.AccountDomain.Interfaces;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
	public class ProfileWriteService : IProfileWriteInterface
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileWriteService(IProfileRepository profileRepository)
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
    }
}

