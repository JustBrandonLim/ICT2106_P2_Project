using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides;

namespace SmartHomeManager.Domain.DeviceDomain.Services.Provides
{
	public class DeviceInformationService : IDeviceInformationService
	{
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceConfigurationRepository _deviceConfigurationRepository;

        public DeviceInformationService(IDeviceRepository deviceRepository, IDeviceConfigurationRepository deviceConfigurationRepository) 
        { 
            _deviceRepository = deviceRepository;
            _deviceConfigurationRepository = deviceConfigurationRepository;
        }

        public async Task<Device?> GetDeviceByIdAsync(Guid deviceId)
        {
            return await _deviceRepository.GetAsync(deviceId);
        }

        public async Task<IEnumerable<Device>> GetDevicesInRoomAsync(Guid roomId)
        {
            return (await _deviceRepository.GetAllAsync()).Where(device => device.RoomId == roomId);
        }

        public async Task<bool> IsDeviceOn(Guid deviceId)
        {
            Device? device = await _deviceRepository.GetAsync(deviceId);
            if (device is null)
                return false;

            return (await _deviceConfigurationRepository.GetAsync("Power", deviceId))?.ConfigurationValue == 1;
        }
    }
}

