using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;

namespace SmartHomeManager.API.Controllers.DeviceStoreAPI;

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
    public async Task<ActionResult<IEnumerable<IDeviceProduct>>> GetDeviceProducts()
    {
        return Ok(await _deviceProductService.GetAllDeviceProducts());
    }

    // GET: api/DeviceProducts/winter
    [HttpGet("winter")]
    public async Task<ActionResult<IEnumerable<IDeviceProduct>>> GetWinterDiscountedDeviceProducts()
    {
        return Ok(await _deviceProductService.GetAllDeviceProductsWithWinterDiscount());
    }

    // GET: api/DeviceProducts/summer
    [HttpGet("summer")]
    public async Task<ActionResult<IEnumerable<IDeviceProduct>>> GetSummerDiscountedDeviceProducts()
    {
        return Ok(await _deviceProductService.GetAllDeviceProductsWithSummerDiscount());
    }

    // POST: api/purchaseDevice
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("purchaseDevice")]
    public async Task<IActionResult> PurchaseDeviceProduct(int deviceProductId, int quantity)
    {
        await _deviceProductService.PurchaseDevice(deviceProductId, quantity);

        return Ok("Goodjob");
    }
}