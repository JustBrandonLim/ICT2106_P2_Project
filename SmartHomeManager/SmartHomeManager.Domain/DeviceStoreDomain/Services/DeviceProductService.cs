using System;
using SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;
using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Services
{

    public class DeviceProductService
	{
        private readonly IDeviceProductsRepository _deviceProductRepository;

        public DeviceProductService(IDeviceProductsRepository deviceProductRepository)
        {
            _deviceProductRepository = deviceProductRepository;
        }

        public async Task<IEnumerable<DeviceProduct>> GetAllDeviceProducts()
        {
            var result = await _deviceProductRepository.GetAllDeviceProducts();
           
            return result;
        }


        public async Task PurchaseDevice(int device_id,int quantity)
        {
            var res = await _deviceProductRepository.Get(device_id);
            if (res == null) return;
            res.ProductQuantity = res.ProductQuantity - quantity;
            _deviceProductRepository.UpdateQuantity(res);
            await _deviceProductRepository.SaveChangesAsync();
        }
    }
}

