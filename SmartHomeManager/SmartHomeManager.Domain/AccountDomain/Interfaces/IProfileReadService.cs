using System;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using System.Diagnostics;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
	public interface IProfileReadService
	{
        public Task<IEnumerable<Profile>> GetProfiles();

        public Task<Profile?> GetProfileByProfileId(Guid id);

        public Task<IEnumerable<Profile?>?> GetProfilesByAccountId(Guid id);

        public Task<IEnumerable<Guid>?> GetDevicesByProfileId(Guid id);

        public Task<int> CheckPinByProfileId(ParentControlPin PinInfo);

        public Task<int> CheckAdultProfile(ProfileIdRequest ProfileIdInfo);

    }
}

