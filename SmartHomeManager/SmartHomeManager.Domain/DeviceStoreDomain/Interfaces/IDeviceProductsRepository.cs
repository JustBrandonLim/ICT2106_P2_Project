using System;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;

public interface IDeviceProductsRepository
{
    Task<DeviceProduct?> Get(int deviceId);
    Task<IEnumerable<DeviceProduct>> GetAllDeviceProducts();
    void UpdateQuantity(DeviceProduct device);
    Task SaveChangesAsync();
}

