using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.Domain.DeviceDomain.Services
{
	public class ManageDeviceService
	{
		private readonly IDeviceRepository _deviceRepository;
		private readonly IDeviceConfigurationLookUpRepository _deviceConfigurationLookUpRepository;
		private readonly IDeviceConfigurationRepository _deviceConfigurationRepository;

		public ManageDeviceService(IDeviceRepository deviceRepository, IDeviceConfigurationLookUpRepository deviceConfigurationLookUpRepository, IDeviceConfigurationRepository deviceConfigurationRepository)
		{
			_deviceRepository = deviceRepository;
			_deviceConfigurationLookUpRepository = deviceConfigurationLookUpRepository;
			_deviceConfigurationRepository = deviceConfigurationRepository;
		}

		public async Task<IEnumerable<Device>> GetAllDevicesByAccountAsync(Guid accountId) 
		{
			IEnumerable<Device> devices = (await _deviceRepository.GetAllAsync()).Where(device => device.AccountId == accountId);

			return devices;
		}

		public async Task<Device> GetDeviceByIdAsync(Guid deviceId)
		{
			return await _deviceRepository.GetAsync(deviceId);
		}

		public async Task<IEnumerable<DeviceConfigurationLookUp>> GetDevicePossibleConfigurationsAsync(string deviceBrand, string deviceModel)
		{
			IEnumerable<DeviceConfigurationLookUp> deviceConfigurationLookUps = (await _deviceConfigurationLookUpRepository.GetAllAsync()).Where(deviceConfigurationLookUp => deviceConfigurationLookUp.DeviceBrand == deviceBrand && deviceConfigurationLookUp.DeviceModel == deviceModel);

			return deviceConfigurationLookUps;
		}

		public async Task<IEnumerable<DeviceConfiguration>> GetDeviceConfigurationsAsync(Guid deviceId, string deviceBrand, string deviceModel)
		{
			IEnumerable<DeviceConfiguration> deviceConfigurations = (await _deviceConfigurationRepository.GetAllAsync()).Where(deviceConfiguration => deviceConfiguration.DeviceId == deviceId && deviceConfiguration.DeviceBrand == deviceBrand && deviceConfiguration.DeviceModel == deviceModel);

			return deviceConfigurations;
		}
	}
}

