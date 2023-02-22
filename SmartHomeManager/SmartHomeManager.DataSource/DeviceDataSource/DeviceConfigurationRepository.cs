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
    }
}

