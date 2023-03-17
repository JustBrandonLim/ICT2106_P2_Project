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

        public ManageDeviceController(IDeviceRepository deviceRepository, IDeviceConfigurationLookUpRepository deviceConfigurationLookUpRepository, IDeviceConfigurationRepository deviceConfigurationRepository, IDeviceTypeRepository deviceTypeRepository) 
	    {
            _manageDeviceService = new(deviceRepository, deviceConfigurationLookUpRepository, deviceConfigurationRepository, deviceTypeRepository);
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
            return await _manageDeviceService.GetDevicePossibleConfigurationsAsync(deviceBrand, deviceModel); 
	    }

        [HttpGet("GetDeviceConfigurations/{deviceId}/{deviceBrand}/{deviceModel}")]
        public async Task<string> GetDeviceConfigurations(Guid deviceId, string deviceBrand, string deviceModel) 
	    {
            return await _manageDeviceService.GetDeviceConfigurationsAsync(deviceId, deviceBrand, deviceModel);
	    }

        [HttpPost("ApplyDeviceConfiguration")]
        public async Task<ActionResult> ApplyDeviceConfiguration([FromBody] DeviceConfigurationWebRequest deviceConfigurationWebRequest) 
	    {
            if (await _manageDeviceService.ApplyDeviceConfigurationAsync(deviceConfigurationWebRequest.ConfigurationKey, deviceConfigurationWebRequest.DeviceBrand, deviceConfigurationWebRequest.DeviceModel, deviceConfigurationWebRequest.DeviceId, deviceConfigurationWebRequest.ConfigurationValue))
            {
                return Ok("ApplyDeviceConfiguration() success!");
            }

            return BadRequest("AppleDeviceConfiguration() failed!");
	    }

        [HttpPost("ApplyDeviceMetadata")]
        public async Task<ActionResult> ApplyDeviceMetadata([FromBody] DeviceMetadataWebRequest deviceMetadataWebRequest)
        {
            if (await _manageDeviceService.ApplyDeviceMetadataAsync(deviceMetadataWebRequest.DeviceId, deviceMetadataWebRequest.DeviceName, deviceMetadataWebRequest.DevicePassword, deviceMetadataWebRequest.DeviceTypeName))
            {
                return Ok("ApplyDeviceMetadata() success!");
            }

            return BadRequest("AppleDeviceMetadata() failed!");
        }

        [HttpPost("SetDevicePasswordById")]
        public async Task<ActionResult> SetDevicePasswordById([FromBody] DevicePasswordWebRequest devicePasswordWebRequest)
        {
            if (await _manageDeviceService.SetDevicePasswordByIdAsync(devicePasswordWebRequest.DeviceId, devicePasswordWebRequest.DevicePassword))
            {
                return Ok("SetDevicePasswordById() success!");
            }

            return BadRequest("SetDevicePasswordById() failed!");
        }

        [HttpPost("ExportDeviceConfigurations/{deviceId}/{deviceBrand}/{deviceModel}")]
        public async Task<string> ExportDeviceConfigurations(Guid deviceId, string deviceBrand, string deviceModel)
        {
            return await _manageDeviceService.ExportDeviceConfigurationsAsync(deviceId, deviceBrand, deviceModel); 
	    }

        [HttpPost("ImportDeviceConfigurations/{deviceId}/{deviceConfigurationJson}")]
        public async Task<ActionResult> ImportDeviceConfigurations(Guid deviceId, string deviceConfigurationJson) 
	    {
            if (await _manageDeviceService.ImportDeviceConfigurationsAsync(deviceId, deviceConfigurationJson))
            {
                return Ok("ImportDeviceConfigurations() success!");
	        }

            return BadRequest("ImportDeviceConfigurations() failed!");
	    }
    }
}

