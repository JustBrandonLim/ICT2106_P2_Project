using System;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.DataSource.DeviceStoreDataSource;

public class DeviceProductRepository : IDeviceProductsRepository
{

    private readonly ApplicationDbContext _db;
    private readonly DbSet<DeviceProduct> _dbSet;

    public DeviceProductRepository(ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<DeviceProduct>();
    }


    public async Task<DeviceProduct?> Get(int id)
    {
        var result = await _dbSet.FindAsync(id);

        return result;
    }

    public async Task<IEnumerable<DeviceProduct>> GetAllDeviceProducts()
    {
        IEnumerable<DeviceProduct> query = await _dbSet.ToListAsync();
        return query;
    }

    public void UpdateQuantity(DeviceProduct device)
    {

        _dbSet.Update(device);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }

}

