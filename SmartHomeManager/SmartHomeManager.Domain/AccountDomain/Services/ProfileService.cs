﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
    public class ProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<string> CreateProfile(Profile profile)
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
                    return "Profile created successfully";
                }
            }

            return "Profile not added";
        }
    }
}