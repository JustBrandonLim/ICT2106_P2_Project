using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartHomeManager.Domain.DeviceDomain;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities.DTOs;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Services;

namespace SmartHomeManager.API.Controllers.DeviceAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageDeviceController : Controller
    {
        private readonly ManageDeviceService _manageDeviceService;

        public ManageDeviceController(IDeviceRepository deviceRepository, IDeviceConfigurationLookUpRepository deviceConfigurationLookUpRepository, IDeviceConfigurationRepository deviceConfigurationRepository) 
	    {
            _manageDeviceService = new(deviceRepository, deviceConfigurationLookUpRepository, deviceConfigurationRepository);
	    }

        [HttpGet("GetAllDevicesByAccount/{accountId}")]
        public async Task<IEnumerable<Device>> GetAllDevicesByAccount(Guid accountId)
        {
            return await _manageDeviceService.GetAllDevicesByAccountAsync(accountId);
        }

        [HttpGet("GetDeviceById/{deviceId}")]
        public async Task<Device?> GetDeviceById(Guid deviceId)
        {
            Device? device = await _manageDeviceService.GetDeviceByIdAsync(deviceId);

            return device ?? null;
	    }

        [HttpGet("GetDevicePossibleConfigurations/{deviceBrand}/{deviceModel}")]
        public async Task<string> GetDevicePossibleConfigurations(string deviceBrand, string deviceModel)
        {
            JArray jsonArray  = new DeviceConfigurationLookUpAdapter(await _manageDeviceService.GetDevicePossibleConfigurationsAsync(deviceBrand, deviceModel)).ConvertToJson();
            return JsonConvert.SerializeObject(jsonArray, Formatting.Indented);
	    }

        [HttpGet("GetDeviceConfigurations/{deviceId}/{deviceBrand}/{deviceModel}")]
        public async Task<string> GetDeviceConfigurations(Guid deviceId, string deviceBrand, string deviceModel) 
	    {
            JArray jsonArray  = new DeviceConfigurationAdapter(await _manageDeviceService.GetDeviceConfigurationsAsync(deviceId, deviceBrand, deviceModel)).ConvertToJson();
            return JsonConvert.SerializeObject(jsonArray, Formatting.Indented);
	    }

        [HttpPost("ApplyDeviceConfiguration")]
        public async Task<ActionResult> ApplyDeviceConfiguration([FromBody] DeviceConfigurationWebRequest deviceConfigurationWebRequest) 
	    {
            if (await _manageDeviceService.ApplyDeviceConfiguration(deviceConfigurationWebRequest.ConfigurationKey, deviceConfigurationWebRequest.DeviceBrand, deviceConfigurationWebRequest.DeviceModel, deviceConfigurationWebRequest.DeviceId, deviceConfigurationWebRequest.ConfigurationValue))
            {
                return Ok("ApplyDeviceConfiguration() success!");
            }

            return BadRequest("AppleDeviceConfiguration() failed!");
	    }

        [HttpPost("ApplyDeviceSettings")]
        public async Task<ActionResult> ApplyDeviceSettings([FromBody] DeviceSettingsWebRequest deviceSettingsWebRequest)
        {
            if (await _manageDeviceService.ApplyDeviceSettings(deviceSettingsWebRequest.DeviceId, deviceSettingsWebRequest.DeviceName, deviceSettingsWebRequest.DevicePassword, deviceSettingsWebRequest.DeviceTypeName))
            {
                return Ok("ApplyDeviceSettings() success!");
            }

            return BadRequest("AppleDeviceSettings() failed!");
        }

        [HttpPost("SetDevicePasswordById")]
        public async Task<ActionResult> SetDevicePasswordById([FromBody] DevicePasswordWebRequest devicePasswordWebRequest)
        {
            if (await _manageDeviceService.SetDevicePasswordById(devicePasswordWebRequest.DeviceId, devicePasswordWebRequest.DevicePassword))
            {
                return Ok("SetDevicePasswordById() success!");
            }

            return BadRequest("SetDevicePasswordById() failed!");
        }
    }
}

