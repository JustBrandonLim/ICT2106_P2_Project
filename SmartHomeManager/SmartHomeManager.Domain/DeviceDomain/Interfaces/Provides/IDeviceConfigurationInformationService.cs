using System;
namespace SmartHomeManager.Domain.DeviceDomain.Interfaces.Provides
{
	public interface IDeviceConfigurationInformationService
	{
		public Task<IEnumerable<Guid>> GetAllDeviceIDsAsync();
		public Task<IEnumerable<string>> GetAllConfigurationKeysAsync(Guid deviceId);
		public Task<IEnumerable<string>> GetAllConfigurationValuesAsync(Guid deviceId, string configurationKey);
		public Task<IEnumerable<string>> GetAllValueMeaningsAsync(Guid deviceId, string configurationKey);
	}
}

