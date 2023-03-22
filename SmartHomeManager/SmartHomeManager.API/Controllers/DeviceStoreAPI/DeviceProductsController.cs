using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.DeviceStoreDomain.Services;
using SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;
using SmartHomeManager.Domain.DeviceStoreDomain.Decorator;

namespace SmartHomeManager.API.Controllers.DeviceStoreAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceProductsController : ControllerBase
    {
        private readonly IDeviceProductService _deviceProductService;

        public DeviceProductsController(IDeviceProductService deviceProductService)
        {
            _deviceProductService = deviceProductService;
        }

        // GET: api/DeviceProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IDeviceProducts>>> GetDeviceProducts()
        {
            return Ok(await _deviceProductService.GetAllDeviceProducts());
        }
        // GET: api/DeviceProducts/winter
        [HttpGet("winter")]
        public async Task<ActionResult<IEnumerable<IDeviceProducts>>> GetWinterDiscountedDeviceProducts()
        {
            return Ok(await _deviceProductService.GetAllDeviceProductsWithWinterDiscount());
        }

        // GET: api/DeviceProducts/summer
        [HttpGet("summer")]
        public async Task<ActionResult<IEnumerable<IDeviceProducts>>> GetSummerDiscountedDeviceProducts()
        {
            return Ok(await _deviceProductService.GetAllDeviceProductsWithSummerDiscount());
        }

        // POST: api/purchaseDevice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("purchaseDevice")]
        public async Task<IActionResult> PurchaseDeviceProduct(int deviceId, int quantity)
        {
            await _deviceProductService.PurchaseDevice(deviceId, quantity);

            return Ok("Goodjob");
        }

    }
}