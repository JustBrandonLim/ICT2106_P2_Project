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

        public ManageDeviceController(IDeviceRepository deviceRepository) 
	    {
            _manageDeviceService = new(deviceRepository);
	    }


        [HttpGet("GetAllDevicesByAccount/{accountId}")]
        public async Task<IEnumerable<Device>> GetAllDevicesByAccount(Guid accountId)
        {
            return await _manageDeviceService.GetAllDevicesByAccountAsync(accountId);
        }
    }
}

