using System;
namespace SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides
{
	public interface IDeviceWattsService
	{
		public Task<int> GetDeviceWattsAsync(Guid deviceId);
	}
}

