using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
	public interface IDeviceConfigurationRepository
	{
		public Task<IEnumerable<DeviceConfiguration>> GetAllAsync();

		public Task<DeviceConfiguration?> GetAsync(string configurationKey, Guid deviceId);

		public Task AddAsync(DeviceConfiguration deviceConfiguration);

		public void Update(DeviceConfiguration deviceConfiguration);
	}
}

