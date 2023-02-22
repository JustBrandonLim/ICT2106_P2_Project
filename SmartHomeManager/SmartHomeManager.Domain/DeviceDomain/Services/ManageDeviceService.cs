using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.Domain.DeviceDomain.Services
{
	public class ManageDeviceService
	{
		private readonly IDeviceRepository _deviceRepository;

		public ManageDeviceService(IDeviceRepository deviceRepository)
		{
			_deviceRepository = deviceRepository;
		}

		public async Task<IEnumerable<Device>> GetAllDevicesByAccountAsync(Guid accountId) 
		{
			IEnumerable<Device> devices = (await _deviceRepository.GetAllAsync()).Where(device => device.AccountId == accountId);

			return devices;
		}
	}
}

