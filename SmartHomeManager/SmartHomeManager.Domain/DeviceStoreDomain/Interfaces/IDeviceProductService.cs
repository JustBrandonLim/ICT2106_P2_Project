using System;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Interfaces
{
	public interface IDeviceProductService
	{
        Task<IEnumerable<IDeviceProducts>> GetAllDeviceProducts();
        Task<IEnumerable<IDeviceProducts>> GetAllDeviceProductsWithSummerDiscount();
        Task<IEnumerable<IDeviceProducts>> GetAllDeviceProductsWithWinterDiscount();
        Task PurchaseDevice(int device_id, int quantity);


    }
}

