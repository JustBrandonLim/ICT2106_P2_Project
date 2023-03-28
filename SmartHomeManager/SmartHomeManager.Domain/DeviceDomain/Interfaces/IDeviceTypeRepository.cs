using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
	public interface IDeviceTypeRepository
	{
		public Task AddAsync(DeviceType deviceType);

		public Task<IEnumerable<DeviceType>> GetAllAsync();

		public Task<bool> SaveAsync();
	}
}

