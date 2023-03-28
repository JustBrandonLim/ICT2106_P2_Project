using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.DeviceDataSource
{
    public class DeviceTypeRepository : IDeviceTypeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeviceTypeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(DeviceType deviceType)
        {
            await _applicationDbContext.DeviceTypes.AddAsync(deviceType);
        }

        public async Task<IEnumerable<DeviceType>> GetAllAsync()
        {
            return await _applicationDbContext.DeviceTypes.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }
    }
}
