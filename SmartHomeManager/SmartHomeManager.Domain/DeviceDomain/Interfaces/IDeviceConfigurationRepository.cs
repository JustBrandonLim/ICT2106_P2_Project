using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
	public interface IDeviceConfigurationRepository
	{
		public Task<IEnumerable<DeviceConfiguration>> GetAllAsync();
	}
}

