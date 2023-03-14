using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Mocks;

namespace SmartHomeManager.DataSource.RoomDataSource.Mocks;

public class DeviceRepositoryMock : IDeviceInformationServiceMock
{
    protected readonly ApplicationDbContext _db;

    protected DbSet<Device> _dbSet;

    // ctor Dependency Injection
    public DeviceRepositoryMock(ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<Device>();
    }

    public Device? GetDeviceById(Guid deviceId)
    {
        return _dbSet.Find(deviceId);
    }

    public IEnumerable<Device> GetDevicesInRoom(Guid roomId)
    {
        // load the data
        var allDevices = _dbSet.ToList();

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