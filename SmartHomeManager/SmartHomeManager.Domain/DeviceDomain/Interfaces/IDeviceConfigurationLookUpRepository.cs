using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
    public interface IDeviceConfigurationLookUpRepository
	{
		public Task<IEnumerable<DeviceConfigurationLookUp>> GetAllAsync();
	}
}

