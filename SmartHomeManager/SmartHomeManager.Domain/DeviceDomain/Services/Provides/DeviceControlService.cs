using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides;

namespace SmartHomeManager.Domain.DeviceDomain.Services.Provides
{
	public class DeviceControlService : IDeviceControlService
	{
        private readonly IDeviceConfigurationRepository _deviceConfigurationRepository;

		public DeviceControlService(IDeviceConfigurationRepository deviceConfigurationRepository)
		{
            _deviceConfigurationRepository = deviceConfigurationRepository;
		}

        public async Task<bool> SwitchOffDeviceAsync(Guid deviceId)
        {
			try
			{ 
				DeviceConfiguration? existingDeviceConfiguration = await _deviceConfigurationRepository.GetAsync("Power", deviceId);
				if (existingDeviceConfiguration == null)
					return false;

				existingDeviceConfiguration.ConfigurationValue = 0;
				_deviceConfigurationRepository.Update(existingDeviceConfiguration);

                return await _deviceConfigurationRepository.SaveAsync();
			}
			catch
			{
				return false;
			}
        }

        public async Task<bool> SwitchOnDeviceAsync(Guid deviceId)
        {
			try
			{ 
				DeviceConfiguration? existingDeviceConfiguration = await _deviceConfigurationRepository.GetAsync("Power", deviceId);
				if (existingDeviceConfiguration == null)
					return false;

				existingDeviceConfiguration.ConfigurationValue = 1;
				_deviceConfigurationRepository.Update(existingDeviceConfiguration);

                return await _deviceConfigurationRepository.SaveAsync();
			}
			catch
			{
				return false;
			}
        }
    }
}

