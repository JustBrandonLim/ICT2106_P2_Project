using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
	public interface IManageDeviceService
	{

		public Task<IEnumerable<Device>> GetAllDevicesByAccountAsync(Guid accountId);

		public Task<Device?> GetDeviceByIdAsync(Guid deviceId);

		public Task<string> GetDevicePossibleConfigurationsAsync(string deviceBrand, string deviceModel);

		public Task<string> GetDeviceConfigurationsAsync(Guid deviceId, string deviceBrand, string deviceModel);

		public Task<bool> ApplyDeviceConfigurationAsync(string configurationKey, string deviceBrand, string deviceModel, Guid deviceId, int configurationValue);

		public Task<bool> ApplyDeviceConfigurationsSameTypeAsync(string configurationKey, string deviceBrand, string deviceModel, Guid deviceId, int configurationValue);

		public Task<bool> ApplyDeviceMetadataAsync(Guid deviceId, string deviceName, string devicePassword, string deviceTypeName);

		public Task<bool> SetDevicePasswordByIdAsync(Guid deviceId, string devicePassword);

		public Task<string> ExportDeviceConfigurationsAsync(Guid deviceId, string deviceBrand, string deviceModel);

		public Task<bool> ImportDeviceConfigurationsAsync(Guid deviceId, string deviceConfigurationJson);
	}
}

