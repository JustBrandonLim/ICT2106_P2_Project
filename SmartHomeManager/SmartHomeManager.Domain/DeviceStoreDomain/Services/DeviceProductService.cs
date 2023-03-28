using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.DeviceStoreDomain.Factories;
using SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Services;

public class DeviceProductService : IDeviceProductService
{
    private readonly IDeviceProductsRepository _deviceProductRepository;

    public DeviceProductService(IDeviceProductsRepository deviceProductRepository)
    {
        _deviceProductRepository = deviceProductRepository;
    }

    public async Task<IEnumerable<IDeviceProduct>> GetAllDeviceProducts()
    {
        var result = await _deviceProductRepository.GetAllDeviceProducts();
        return result;
    }

    public async Task<IEnumerable<IDeviceProduct>> GetAllDeviceProductsWithSummerDiscount()
    {
        var products = await _deviceProductRepository.GetAllDeviceProducts();
        var summerDiscountedProducts = new List<IDeviceProduct>();

        foreach (var product in products)
            summerDiscountedProducts.Add(ProductFactory.CreateProductWithSummerDiscountDecorator(product));

        return summerDiscountedProducts;
    }

    public async Task<IEnumerable<IDeviceProduct>> GetAllDeviceProductsWithWinterDiscount()
    {
        var products = await _deviceProductRepository.GetAllDeviceProducts();
        var winterDiscountedProducts = new List<IDeviceProduct>();

        foreach (var product in products)
            winterDiscountedProducts.Add(ProductFactory.CreateProductWithWinterDiscountDecorator(product));

        return winterDiscountedProducts;
    }

    public async Task PurchaseDevice(int deviceProductId, int quantity)
    {
        var res = await _deviceProductRepository.Get(deviceProductId);
        if (res == null) return;
        res.ProductQuantity -= quantity;
        _deviceProductRepository.UpdateQuantity(res);
        await _deviceProductRepository.SaveChangesAsync();
    }
}