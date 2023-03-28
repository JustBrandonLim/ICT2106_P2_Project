using SmartHomeManager.Domain.DeviceStoreDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;

public interface IDeviceProductService
{
    Task<IEnumerable<IDeviceProduct>> GetAllDeviceProducts();
    Task<IEnumerable<IDeviceProduct>> GetAllDeviceProductsWithSummerDiscount();
    Task<IEnumerable<IDeviceProduct>> GetAllDeviceProductsWithWinterDiscount();
    Task PurchaseDevice(int deviceProductId, int quantity);
}