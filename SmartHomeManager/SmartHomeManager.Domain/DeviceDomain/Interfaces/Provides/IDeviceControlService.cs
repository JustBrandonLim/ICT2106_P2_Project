using System;
namespace SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides
{
	public interface IDeviceControlService
	{
		public Task<bool> SwitchOnDeviceAsync(Guid deviceId);

		public Task<bool> SwitchOffDeviceAsync(Guid deviceId);
	}
}

