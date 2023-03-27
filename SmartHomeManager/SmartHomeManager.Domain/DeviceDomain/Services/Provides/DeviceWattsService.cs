using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides;

namespace SmartHomeManager.Domain.DeviceDomain.Services.Provides
{
	public class DeviceWattsService : IDeviceWattsService
	{
		private readonly IDeviceRepository _deviceRepository;

		public DeviceWattsService(IDeviceRepository deviceRepository)
		{
			_deviceRepository = deviceRepository;
		}

        public async Task<int> GetDeviceWatts(Guid deviceId)
        {
			Device? device = await _deviceRepository.GetAsync(deviceId);
			
			return device == null ? -1 : device.DeviceWatts;
        }
    }
}

