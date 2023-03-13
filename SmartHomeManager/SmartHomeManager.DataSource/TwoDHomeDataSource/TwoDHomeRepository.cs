using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

namespace SmartHomeManager.DataSource.TwoDHomeDataSource;

public class TwoDHomeRepository : ITwoDHomeRepository
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<DeviceCoordinate> _dbSetDeviceCoordinate;
    private readonly DbSet<Room> _dbSetRoom;
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
        foreach (var room in result)
        {
            if (room.RoomCoordinate == null)
                continue;
            ret.Add(room.RoomCoordinate);
        }

        return ret;
    }

    public async Task AddRange(IEnumerable<IRoomCoordinate> entities)
    {
        var roomCoordinates = entities.ToList();
        var entitiesToBeUpdated = new List<RoomCoordinate>();
        foreach (var entity in roomCoordinates) entitiesToBeUpdated.Add((RoomCoordinate)entity);
        _dbSetRoomCoordinate.AddRange(entitiesToBeUpdated);
        await SaveChangesAsync();
    }

    public async Task RemoveRange(IEnumerable<IRoomCoordinate> entities)
    {
        var roomCoordinates = entities.ToList();
        var entitiesToBeUpdated = new List<RoomCoordinate>();
        foreach (var entity in roomCoordinates) entitiesToBeUpdated.Add((RoomCoordinate)entity);
        _dbSetRoomCoordinate.RemoveRange(entitiesToBeUpdated);
        await SaveChangesAsync();
    }

    // for testing purposes
    public async Task<IEnumerable<DeviceCoordinate>> GetAllDeviceCoordinates()
    {
        IEnumerable<DeviceCoordinate> query = await _dbSetDeviceCoordinate.ToListAsync();
        return query;
    }

    public async Task<IEnumerable<RoomCoordinate>> GetAllRoomCoordinates()
    {
        IEnumerable<RoomCoordinate> query = await _dbSetRoomCoordinate.ToListAsync();
        return query;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}