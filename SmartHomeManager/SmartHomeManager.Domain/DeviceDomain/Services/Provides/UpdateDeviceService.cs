using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides;

namespace SmartHomeManager.Domain.DeviceDomain.Services.Provides
{
	public class UpdateDeviceService : IUpdateDeviceService
	{
		private readonly IDeviceRepository _deviceRepository;

		public UpdateDeviceService(IDeviceRepository deviceRepository)
		{
			_deviceRepository = deviceRepository;
		}

		public async Task<bool> AddDeviceToRoomAsync(Guid deviceId, Guid roomId)
		{
			try
			{ 
				Device? existingDevice = await _deviceRepository.GetAsync(deviceId);
				if (existingDevice == null)
					return false;

				existingDevice.RoomId = roomId;
				_deviceRepository.Update(existingDevice);

                return await _deviceRepository.SaveAsync();
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> RemoveDeviceFromRoomAsync(Guid deviceId)
		{ 
			try
			{ 
				Device? existingDevice = await _deviceRepository.GetAsync(deviceId);
				if (existingDevice == null)
					return false;

				existingDevice.RoomId = null;
				_deviceRepository.Update(existingDevice);

                return await _deviceRepository.SaveAsync();
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> AddDeviceToAccountAsync(Guid deviceId, Guid accountId)	
		{ 
			try
			{ 
				Device? existingDevice = await _deviceRepository.GetAsync(deviceId);
				if (existingDevice == null)
					return false;

				existingDevice.AccountId = accountId;
				_deviceRepository.Update(existingDevice);

                return await _deviceRepository.SaveAsync();
			}
			catch
			{
				return false;
			}
		}
    }
}

