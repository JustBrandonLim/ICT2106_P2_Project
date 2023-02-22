using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.DeviceDomain.Entities;
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
        public async Task<Device> GetDeviceById(Guid deviceId)
        {
            /* TEST */


            return await _manageDeviceService.GetDeviceByIdAsync(deviceId);
	    }

        // config
        [HttpGet("GetDevicePossibleConfigurations/{deviceBrand}/{deviceModel}")]
        public async Task<IEnumerable<DeviceConfigurationLookUp>> GetDevicePossibleConfigurations(string deviceBrand, string deviceModel) 
	    {
            return await _manageDeviceService.GetDevicePossibleConfigurationsAsync(deviceBrand, deviceModel);
	    }

        [HttpGet("GetDeviceConfigurations/{deviceId}/{deviceBrand}/{deviceModel}")]
        public async Task<IEnumerable<DeviceConfiguration>> GetDeviceConfigurations(Guid deviceId, string deviceBrand, string deviceModel) 
	    {
            return await _manageDeviceService.GetDeviceConfigurationsAsync(deviceId, deviceBrand, deviceModel);
	    }
    }
}

