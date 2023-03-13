using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Mocks;

namespace SmartHomeManager.DataSource.RoomDataSource.Mocks;

public class DeviceRepositoryMock : IDeviceInformationServiceMock
{
    protected readonly ApplicationDbContext _db;

    // NOT IN USE, for note taking / learning purposes only
    // internal means only accessible within the same assembly
    // internal DbSet<T> _dbSet

    // dbSet of type T
    protected DbSet<Device> _dbSet;

    // ctor Dependency Injection
    public DeviceRepositoryMock(ApplicationDbContext db)
    {
        _db = db;

        // sets it to the generic class of the repository
        _dbSet = _db.Set<Device>();
    }

    public IEnumerable<Device> GetDevicesInRoom(Guid roomId)
    {
        // load the data
        var allDevices = _db.Devices.ToList();

        // filter the data
        var result = allDevices.Where(device => device.RoomId == roomId);

        return result;
    }

    
    // dummy implementation, actual implementation is coming from team 4 (Device Team)
    // this is for internal testing purposes only
    public bool isDeviceOn(Guid deviceId)
    {
        return false;
    }
}