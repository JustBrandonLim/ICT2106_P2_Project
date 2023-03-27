using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides;

namespace SmartHomeManager.Domain.DeviceDomain.Services.Provides
{
	public class DeviceConfigurationInformationService : IDeviceConfigurationInformationService
	{
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceConfigurationLookUpRepository _deviceConfigurationLookUpRepository;

		public DeviceConfigurationInformationService(IDeviceRepository deviceRepository, IDeviceConfigurationLookUpRepository deviceConfigurationLookUpRepository)
		{
            _deviceRepository = deviceRepository;
            _deviceConfigurationLookUpRepository = deviceConfigurationLookUpRepository;
		}

        public async Task<IEnumerable<string>> GetAllConfigurationKeysAsync(Guid deviceId)
        {
            Device? device = await _deviceRepository.GetAsync(deviceId);

            if (device == null)
                return new List<string>();

            return (await _deviceConfigurationLookUpRepository.GetAllAsync())
		            .Where(deviceConfigurationLookUp => deviceConfigurationLookUp.DeviceBrand == device.DeviceBrand && deviceConfigurationLookUp.DeviceModel == device.DeviceModel)
		            .Select(deviceConfigurationLookup => deviceConfigurationLookup.ConfigurationKey);
        }

        public async Task<IEnumerable<string>> GetAllConfigurationValuesAsync(Guid deviceId, string configurationKey)
        {
            Device? device = await _deviceRepository.GetAsync(deviceId);

            if (device == null)
                return new List<string>();

            return (await _deviceConfigurationLookUpRepository.GetAllAsync())
		            .Where(deviceConfigurationLookUp => deviceConfigurationLookUp.DeviceBrand == device.DeviceBrand && deviceConfigurationLookUp.DeviceModel == device.DeviceModel && deviceConfigurationLookUp.ConfigurationKey == configurationKey)
		            .Select(deviceConfigurationLookup => deviceConfigurationLookup.ConfigurationValue);
        }

        public async Task<IEnumerable<Guid>> GetAllDeviceIDsAsync()
        {
            return (await _deviceRepository.GetAllAsync()).Select(device => device.DeviceId);
        }

        public async Task<IEnumerable<string>> GetAllValueMeaningsAsync(Guid deviceId, string configurationKey)
        {
            Device? device = await _deviceRepository.GetAsync(deviceId);

            if (device == null)
                return new List<string>();

            return (await _deviceConfigurationLookUpRepository.GetAllAsync())
		            .Where(deviceConfigurationLookUp => deviceConfigurationLookUp.DeviceBrand == device.DeviceBrand && deviceConfigurationLookUp.DeviceModel == device.DeviceModel && deviceConfigurationLookUp.ConfigurationKey == configurationKey)
		            .Select(deviceConfigurationLookup => deviceConfigurationLookup.ConfigurationValue);
        }
    }
}

