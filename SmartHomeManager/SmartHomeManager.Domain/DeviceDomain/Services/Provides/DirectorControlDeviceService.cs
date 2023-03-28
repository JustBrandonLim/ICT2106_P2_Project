using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides;

namespace SmartHomeManager.Domain.DeviceDomain.Services.Provides
{
	public class DirectorControlDeviceService : IDirectorControlDeviceService
	{
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceConfigurationRepository _deviceConfigurationRepository;

		public DirectorControlDeviceService(IDeviceRepository deviceRepository, IDeviceConfigurationRepository deviceConfigurationRepository)
		{
            _deviceRepository = deviceRepository;
            _deviceConfigurationRepository = deviceConfigurationRepository;
        }

        public async Task<bool> SetDeviceTypeConfigurationAsync(Guid accountId, string deviceTypeName, string configurationKey, int configurationValue)
        {
            try
            {
                IEnumerable<Device> devices = (await _deviceRepository.GetAllAsync())
                                                .Where(device => device.AccountId == accountId && device.DeviceTypeName == deviceTypeName);

                foreach (Device device in devices)
                {
                    DeviceConfiguration? deviceConfiguration = await _deviceConfigurationRepository.GetAsync(configurationKey, device.DeviceId);
                    if (deviceConfiguration == null) return false;

                    deviceConfiguration.ConfigurationValue = configurationValue;

                    _deviceConfigurationRepository.Update(deviceConfiguration);

                }

                return await _deviceConfigurationRepository.SaveAsync();
            }
            catch 
	        {
                return false;
	        }
        }
    }
}

