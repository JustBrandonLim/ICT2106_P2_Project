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


namespace SmartHomeManager.API.Controllers.DeviceStoreAPI;

[Route("api/[controller]")]
[ApiController]
public class DeviceProductsController : ControllerBase
{
    private readonly  DeviceProductService _deviceProductService;

    public DeviceProductsController(IDeviceProductsRepository deviceProductsRepository)
    {
        _deviceProductService = new DeviceProductService(deviceProductsRepository);
    }

    // GET: api/DeviceProducts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeviceProduct>>> GetDeviceProducts()
    {
        return Ok(await _deviceProductService.GetAllDeviceProducts());
    }

    // POST: api/purchaseDevice
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("purchaseDevice")]
    public async Task<ActionResult<DeviceProduct>> PurchaseDeviceProduct(int deviceId, int quantity)
    {
        await _deviceProductService.PurchaseDevice(deviceId,quantity);

        return Ok("Goodjob");
    }
    
}