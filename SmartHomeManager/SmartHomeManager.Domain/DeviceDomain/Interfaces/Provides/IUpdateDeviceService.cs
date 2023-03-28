using System;
namespace SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides
{
	public interface IUpdateDeviceService
	{
        public Task<bool> AddDeviceToRoomAsync(Guid deviceId, Guid roomId);

        public Task<bool> RemoveDeviceFromRoomAsync(Guid deviceId);

        public Task<bool> AddDeviceToAccountAsync(Guid deviceId, Guid accountId);
    }
}

