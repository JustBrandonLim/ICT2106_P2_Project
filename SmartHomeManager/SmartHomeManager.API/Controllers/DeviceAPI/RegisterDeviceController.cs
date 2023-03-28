using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.DeviceDomain.Services;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities.DTOs;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.DeviceAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterDeviceController : ControllerBase
    {
        private readonly IRegisterDeviceService _registerDeviceService;

        public RegisterDeviceController(IRegisterDeviceService registerDeviceService) 
        {
            _registerDeviceService = registerDeviceService;
        }

        // GET api/<RegisterDeviceController>/GetAllDeviceTypes
        [HttpGet("GetAllDeviceTypes")]
        public async Task<IEnumerable<string>> GetAllDeviceTypes()
        {
            IEnumerable<DeviceType> deviceTypes = await _registerDeviceService.GetAllDevicesTypeAsync();
            return deviceTypes.Select(deviceType => deviceType.DeviceTypeName);    
        }

        // POST api/<RegisterDeviceController>/RegisterDevice
        [HttpPost("RegisterDevice")]
        public async Task<ActionResult> RegisterDevice([FromBody] DeviceWebRequest deviceWebRequest)
        {
            if (await _registerDeviceService.RegisterDeviceAsync(deviceWebRequest.DeviceName, deviceWebRequest.DeviceBrand, deviceWebRequest.DeviceModel, deviceWebRequest.DeviceTypeName, deviceWebRequest.DeviceWatts, deviceWebRequest.DeviceSerialNumber, deviceWebRequest.AccountId))
            {
                return Ok("RegisterDevice() success!");
	        }

            return BadRequest("RegisterDevice() failed!");
        }

        // POST api/<RegisterDeviceController>/AddDeviceType
        [HttpPost("AddDeviceType")]
        public async Task<ActionResult> AddDeviceType([FromBody] DeviceType deviceType)
        {
            if (await _registerDeviceService.AddDeviceTypeAsync(deviceType))
            {
                return Ok("AddDeviceType() success!");
	        }

            return BadRequest("AddDeviceType() failed!");
        }
    }
}
