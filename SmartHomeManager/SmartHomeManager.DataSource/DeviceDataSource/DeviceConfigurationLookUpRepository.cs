using System;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.DataSource.DeviceDataSource
{
    public class DeviceConfigurationLookUpRepository : IDeviceConfigurationLookUpRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeviceConfigurationLookUpRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<DeviceConfigurationLookUp>> GetAllAsync()
        {
            return await _applicationDbContext.DeviceConfigurationLookUps.ToListAsync();
        }
	}
}

