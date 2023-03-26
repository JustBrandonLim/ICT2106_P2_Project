using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides
{
	public interface IDeviceInformationService
	{
		public Task<Device?> GetDeviceByIdAsync(Guid deviceId);

		public Task<IEnumerable<Device>> GetDevicesInRoomAsync(Guid roomId);

		public Task<bool> IsDeviceOnAsync(Guid deviceId);

		public Task<IEnumerable<Device>> GetAllDevicesByAccountAsync(Guid accountId); 
    }
}

