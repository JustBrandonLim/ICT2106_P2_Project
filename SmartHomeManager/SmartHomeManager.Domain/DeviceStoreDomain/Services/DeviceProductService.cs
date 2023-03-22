using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.DeviceStoreDomain.Decorator;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;
using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Services
{
    public class DeviceProductService : IDeviceProductService
    {
        private readonly IDeviceProductsRepository _deviceProductRepository;

        public DeviceProductService(IDeviceProductsRepository deviceProductRepository)
        {
            _deviceProductRepository = deviceProductRepository;
        }

        public async Task<IEnumerable<IDeviceProducts>> GetAllDeviceProducts()
        {
            var result = await _deviceProductRepository.GetAllDeviceProducts();
            return result;
        }

        public async Task<IEnumerable<IDeviceProducts>> GetAllDeviceProductsWithSummerDiscount()
        {
            var products = await _deviceProductRepository.GetAllDeviceProducts();
            var summerDiscountedProducts = new List<IDeviceProducts>();

            foreach (var product in products)
            {
                summerDiscountedProducts.Add(new SummerDiscountDecorator(product));
            }

            return summerDiscountedProducts;
        }

        public async Task<IEnumerable<IDeviceProducts>> GetAllDeviceProductsWithWinterDiscount()
        {
            var products = await _deviceProductRepository.GetAllDeviceProducts();
            var winterDiscountedProducts = new List<IDeviceProducts>();

            foreach (var product in products)
            {
                winterDiscountedProducts.Add(new WinterDiscountDecorator(product));
            }

            return winterDiscountedProducts;
        }

        public async Task PurchaseDevice(int device_id, int quantity)
        {
            var res = await _deviceProductRepository.Get(device_id);
            if (res == null) return;
            res.ProductQuantity = res.ProductQuantity - quantity;
            _deviceProductRepository.UpdateQuantity(res);
            await _deviceProductRepository.SaveChangesAsync();
        }
    }
}
