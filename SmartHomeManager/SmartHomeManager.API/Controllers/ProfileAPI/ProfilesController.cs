using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.DataSource.ProfileDataSource;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Services;

namespace SmartHomeManager.API.Controllers.ProfileAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileReadService _profileReadService;
        private readonly IProfileWriteService _profileWriteService;
        private readonly IScenarioServices _scenarioServices;

        public ProfilesController(IProfileReadService profileReadService, IProfileWriteService profileWriteService, 
            IScenarioServices scenarioServices)
        {
            /*_profileService = profileService ?? throw new ArgumentNullException("profile service null");*/
            _profileReadService = profileReadService;
            _profileWriteService = profileWriteService;
            _scenarioServices = scenarioServices;
        }

        //public ProfilesController(IProfileReadInterface profileReadInterface, IProfileWriteInterface profileWriteInterface)
        //{
        //    /*_profileService = profileService ?? throw new ArgumentNullException("profile service null");*/
        //    _profileService = profileReadInterface;
        //    _scenarioServices = profileWriteInterface;
        //}

        /* 
         * GET: api/Profiles
         * Return: 
         * Ok(profiles) - IEnumerable of Profiles
         * NotFound(1) - No Profiles in DB
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            IEnumerable<Profile> profiles = await _profileReadService.GetProfiles();
            
            if (!profiles.Any())
                return NotFound();

            return Ok(profiles);
        }

        /* 
         * GET: api/Profiles/22222222-2222-2222-2222-222222222222
         * Return: 
         * Ok(profile) - Profile as requested
         * NotFound(1) - Profile does not exist
        */
        [HttpGet("{profileId}")]
        public async Task<ActionResult<Profile>> GetProfileByProfileId(Guid profileId)
        {
            Profile? profile = await _profileReadService.GetProfileByProfileId(profileId);

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        /* 
         * GET: api/Profiles/get-profiles/11111111-1111-1111-1111-111111111111
         * Return: 
         * Ok(profiles) - IEnumerable of Profiles
         * NotFound(1) - No Profiles in DB
        */
        [HttpGet("get-profiles/{accountId}")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfilesByAccountId(Guid accountId)
        {
            /*var realAccountId = Guid.Parse(accountId);*/
            IEnumerable<Profile> profiles = (await _profileReadService.GetProfilesByAccountId(accountId))!;

            if (profiles == null)
                return NotFound(1);

            return Ok(profiles);
        }

        /* 
         * GET: api/Profiles/get-device-ids/22222222-2222-2222-2222-222222222222
         * Return: 
         * Ok(listOfDeviceIds) - IEnumerable of Device Ids
         * NotFound(1) - No Device/Profile in DeviceProfile r/s table in DB
        */
        [HttpGet("get-device-ids/{profileId}")]
        public async Task<ActionResult<IEnumerable<Guid>>> GetDevicesByProfileId(Guid profileId)
        {
            IEnumerable<Guid> listOfDeviceIds = (await _profileReadService.GetDevicesByProfileId(profileId))!;

            if (listOfDeviceIds == null)
            {
                return NotFound(1);
            }

            return Ok(listOfDeviceIds);
        }

        [HttpPost("check-Pin")] // Go to check-pin method. if it's check-pin/{profileId} means need to key in profileId
        public async Task<ActionResult> ValidatePinByProfileId([FromBody] ParentControlPin pinInfo)
        {
            int response = await _profileReadService.CheckPinByProfileId(pinInfo);
                Debug.WriteLine("response is : " + response);
            if (response == 1)
            {
                return Ok(1);   // child profile correct pin
            }
            else if (response == 2)
            {
                return Ok(2);   // child profile wrong pin
            }
            return BadRequest(3); 
        }

        // check if the profile holder is adult or child
        [HttpPost("adult-checker")] 
        public async Task<ActionResult> ValidateAdult([FromBody] ProfileIdRequest profileIdInfo)
        {
            int response = await _profileReadService.CheckAdultProfile(profileIdInfo);
            Debug.WriteLine("response is : " + response);
            if (response == 1)
            {
                return Ok(1);   // adult profile
            }
            else {
                return Ok(2);   // child profile 
            }
        }


        /*
                * POST: api/Profiles
                * Return:
                * Ok(1) - Profile created successfully
                * BadRequest(1) - Profile failed to create
                */
        [HttpPost]
        public async Task<ActionResult> PostProfile([FromBody] ProfileWebRequest profileWebRequest)
        {
            int response = await _profileWriteService.CreateProfile(profileWebRequest);

            if (response == 1)
            {
                return Ok(1);
            }

            return BadRequest(1);
        }

        /* 
         * PUT: api/Profiles/11111111-1111-1111-1111-111111111111
         * Return:
         * Ok(1) - Profile successfully updated
         * BadRequest(1) - Profile failed to update
         * NotFound(1) - Profile does not exist
         * 
        */
        [HttpPut("{profileId}")]
        public async Task<IActionResult> PutProfile(Guid profileId, [FromBody] UpdateProfileWebRequest updateProfileWebRequest)
        {
           Profile? profile = await _profileReadService.GetProfileByProfileId(profileId);
             if (profile == null)
             {
                 return NotFound(1);
             }

             if (await _profileWriteService.UpdateProfile(profile, updateProfileWebRequest))
             {
                 return Ok(1);
             }

             return BadRequest(1);
        }
        /* 
         * DELETE: api/Profiles/11111111-1111-1111-1111-111111111111
         * Return:
         * Ok(1) - Profile successfully deleted
         * BadRequest(1) - Profile failed to delete
         * NotFound(1) - Profile does not exist
         * 
        */
        [HttpDelete("{profileId}")]
        public async Task<IActionResult> DeleteProfile(Guid profileId)
        {

            Profile? profile = await _profileReadService.GetProfileByProfileId(profileId);
            if (profile == null)
            {
                return NotFound(1);
            }

            if (await _profileWriteService.DeleteProfile(profile))
            {
                return Ok(1);
            }

            return BadRequest(1);
        }

        [HttpPut("share-profile/{profileId}")]
        public async Task<ActionResult<string>> Put(Guid profileId)
        {
            bool success = await _scenarioServices.UpdateScenarioByProfileId(profileId);
            if (success)
            {
                return Ok("Scenarios shared!");
            }
            return BadRequest("Scenarios not shared");
        }
    }
}
