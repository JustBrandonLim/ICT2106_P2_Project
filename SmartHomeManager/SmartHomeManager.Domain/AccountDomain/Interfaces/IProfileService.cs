using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IProfileService
    {
        public Task<int> CreateProfile(ProfileWebRequest profileWebRequest);
        public Task<IEnumerable<Profile>> GetProfiles();
        public Task<Profile?> GetProfileByProfileId(Guid id);
        public Task<IEnumerable<Profile?>?> GetProfilesByAccountId(Guid id);
        public Task<IEnumerable<Guid>?> GetDevicesByProfileId(Guid id);
        public Task<bool> UpdateProfile(Profile profile, UpdateProfileWebRequest updateProfileWebRequest);
        public Task<bool> DeleteProfile(Profile profile);
        public Task<int> CheckPinByProfileId(ParentControlPin pinInfo);
        public Task<int> CheckAdultProfile(ProfileIdRequest ProfileIdInfo);


    }
}
