using SmartHomeManager.Domain.DeviceStoreDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;

public interface IDeviceProductsRepository
{
    Task<IDeviceProduct?> Get(int deviceId);
    Task<IEnumerable<IDeviceProduct>> GetAllDeviceProducts();
    void UpdateQuantity(IDeviceProduct device);
    Task SaveChangesAsync();
}