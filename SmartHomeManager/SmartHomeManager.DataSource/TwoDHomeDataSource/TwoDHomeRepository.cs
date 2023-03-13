using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

namespace SmartHomeManager.DataSource.TwoDHomeDataSource;

public class TwoDHomeRepository : ITwoDHomeRepository
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<DeviceCoordinate> _dbSetDeviceCoordinate;
    private readonly DbSet<RoomCoordinate> _dbSetRoomCoordinate;

    public TwoDHomeRepository(ApplicationDbContext db)
    {
        _db = db;
        _dbSetDeviceCoordinate = _db.Set<DeviceCoordinate>();
        _dbSetRoomCoordinate = _db.Set<RoomCoordinate>();
    }

    public async Task<DeviceCoordinate?> GetDeviceCoordinate(Guid id)
    {
        var result = await _dbSetDeviceCoordinate.FindAsync(id);
        return result;
    }

    public async Task<IEnumerable<DeviceCoordinate>> GetAllDeviceCoordinates()
    {
        IEnumerable<DeviceCoordinate> query = await _dbSetDeviceCoordinate.ToListAsync();
        return query;
    }

    public void UpdateDeviceCoordinate(DeviceCoordinate deviceCoordinate)
    {
        _dbSetDeviceCoordinate.Update(deviceCoordinate);
    }

    public void RemoveDeviceCoordinate(DeviceCoordinate deviceCoordinate)
    {
        _dbSetDeviceCoordinate.Remove(deviceCoordinate);
    }


    public async Task<RoomCoordinate?> GetRoomCoordinate(Guid id)
    {
        var result = await _dbSetRoomCoordinate.FindAsync(id);
        return result;
    }

    public async Task<IEnumerable<RoomCoordinate>> GetAllRoomCoordinates()
    {
        IEnumerable<RoomCoordinate> query = await _dbSetRoomCoordinate.ToListAsync();
        return query;
    }

    public void UpdateRoomCoordinate(RoomCoordinate roomCoordinate)
    {
        _dbSetRoomCoordinate.Update(roomCoordinate);
    }

    public void RemoveRoomCoordinate(RoomCoordinate roomCoordinate)
    {
        _dbSetRoomCoordinate.Remove(roomCoordinate);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}