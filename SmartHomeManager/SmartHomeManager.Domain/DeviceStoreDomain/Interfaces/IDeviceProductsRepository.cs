using System;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;

public interface IDeviceProductsRepository
{
    Task<IDeviceProducts?> Get(int deviceId);
    Task<IEnumerable<IDeviceProducts>> GetAllDeviceProducts();
    void UpdateQuantity(IDeviceProducts device);
    Task SaveChangesAsync();
}

