using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.DataSource.DeviceDataSource
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeviceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(Device device) 
	    {
            await _applicationDbContext.Devices.AddAsync(device);
	    }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _applicationDbContext.Devices.ToListAsync();
        }
        
        public async Task<Device?> GetAsync(Guid deviceId)
        {
            return await _applicationDbContext.Devices.FindAsync(deviceId);
        }

        public async Task<bool> SaveAsync() 
	    {
            return await _applicationDbContext.SaveChangesAsync() > 0;
	    }

        public void Update(Device device)
        {
            _applicationDbContext.Devices.Update(device);
        }
    }
}
