using System;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.DataSource.DeviceDataSource
{
	public class DeviceConfigurationRepository : IDeviceConfigurationRepository
	{
        private readonly ApplicationDbContext _applicationDbContext;

		public DeviceConfigurationRepository(ApplicationDbContext applicationDbContext)
		{
            _applicationDbContext = applicationDbContext;
		}

        public async Task<IEnumerable<DeviceConfiguration>> GetAllAsync()
        {
            return await _applicationDbContext.DeviceConfigurations.ToListAsync();
        }

        public async Task<DeviceConfiguration?> GetAsync(string configurationKey, Guid deviceId) 
	    {
            return await _applicationDbContext.DeviceConfigurations.FindAsync(configurationKey, deviceId);
	    }

        public async Task AddAsync(DeviceConfiguration deviceConfiguration) 
	    {
            await _applicationDbContext.DeviceConfigurations.AddAsync(deviceConfiguration);
	    }

        public void Update(DeviceConfiguration deviceConfiguration)
        {
            _applicationDbContext.DeviceConfigurations.Update(deviceConfiguration);
        }

        public async Task<bool> SaveAsync() 
	    {
            return await _applicationDbContext.SaveChangesAsync() > 0;
	    }
    }
}

