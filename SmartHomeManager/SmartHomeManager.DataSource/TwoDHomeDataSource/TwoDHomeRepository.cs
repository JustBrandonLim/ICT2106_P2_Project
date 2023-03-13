using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

namespace SmartHomeManager.DataSource.TwoDHomeDataSource;

public class TwoDHomeRepository : ITwoDHomeRepository
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<Room> _dbSetRoom;
    private readonly DbSet<DeviceCoordinate> _dbSetDeviceCoordinate;
    private readonly DbSet<RoomCoordinate> _dbSetRoomCoordinate;

    public TwoDHomeRepository(ApplicationDbContext db)
    {
        _db = db;
        _dbSetRoom = _db.Set<Room>();
        _dbSetDeviceCoordinate = _db.Set<DeviceCoordinate>();
        _dbSetRoomCoordinate = _db.Set<RoomCoordinate>();
    }

    public List<IRoomCoordinate> GetAllRoomCoordinatesRelatedToAccount(Guid accountId)
    {
        // load the data, including the room coordinates
        var allRooms = _dbSetRoom.Include(r => r.RoomCoordinate).ToList();

        // filter the data
        var result = allRooms.Where(room => room.AccountId == accountId);

        var ret = new List<IRoomCoordinate>();
        foreach (var room in result) ret.Add(room.RoomCoordinate);
        return ret;
    }

    public async Task<bool> AddDeviceCoordinate(IDeviceCoordinate deviceCoordinate)
    {
        _dbSetDeviceCoordinate.Add((DeviceCoordinate)deviceCoordinate);
        await SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddRoomCoordinate(IRoomCoordinate roomCoordinate)
    {
        _dbSetRoomCoordinate.Add((RoomCoordinate)roomCoordinate);
        await SaveChangesAsync();
        return true;
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

    public async Task AddRange(IEnumerable<IRoomCoordinate> entities)
    {
        var entitiesToBeUpdated = new List<RoomCoordinate>();
        foreach (var entity in entities) entitiesToBeUpdated.Add((RoomCoordinate)entity);
        _dbSetRoomCoordinate.AddRange(entitiesToBeUpdated);
        await SaveChangesAsync();
    }

    public async Task RemoveRange(IEnumerable<IRoomCoordinate> entities)
    {
        var entitiesToBeUpdated = new List<RoomCoordinate>();
        foreach (var entity in entities) entitiesToBeUpdated.Add((RoomCoordinate)entity);
        _dbSetRoomCoordinate.RemoveRange(entitiesToBeUpdated);
        await SaveChangesAsync();
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