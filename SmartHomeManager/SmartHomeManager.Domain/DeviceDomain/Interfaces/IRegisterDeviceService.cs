using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
	public interface IRegisterDeviceService
	{

        public Task<IEnumerable<DeviceType>> GetAllDevicesTypeAsync();

        public Task<bool> RegisterDeviceAsync(string deviceName, string deviceBrand, string deviceModel, string deviceTypeName, int deviceWatts, string deviceSerialNumber, Guid accountId);

        public Task<bool> AddDeviceTypeAsync(DeviceType deviceType);
	}
}

