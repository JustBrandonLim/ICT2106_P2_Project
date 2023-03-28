using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceStoreDomain.Decorators;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;

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


    public async Task<IDeviceProduct?> Get(int id)
    {
        var result = await _dbSet.FindAsync(id);


        return result;
    }

    public async Task<IEnumerable<IDeviceProduct>> GetAllDeviceProducts()
    {
        IEnumerable<DeviceProduct> query = await _dbSet.ToListAsync();

        return query;
    }

    public void UpdateQuantity(IDeviceProduct device)
    {
        if (device is ProductDiscountDecorator decorator)
        {
            var undecoratedDevice = decorator.UndecoratedProduct;
            _dbSet.Update(undecoratedDevice);
        }
        // _dbSet.Update(device);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}