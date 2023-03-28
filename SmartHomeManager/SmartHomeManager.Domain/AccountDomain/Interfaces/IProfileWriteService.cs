using System;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Factory;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
	public interface IProfileWriteService
	{
        public Task<int> CreateProfile(ProfileWebRequest profileWebRequest);

        public Task<bool> UpdateProfile(Profile profile, UpdateProfileWebRequest updateProfileWebRequest);

        public Task<bool> DeleteProfile(Profile profile);
    }
}

