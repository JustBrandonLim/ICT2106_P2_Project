using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Interfaces;

namespace SmartHomeManager.DataSource.RoomDataSource;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<Room> _dbSet;

    public RoomRepository(ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<Room>();
    }

    public async Task<IRoom?> Get(Guid id)
    {
        var result = await _dbSet.FindAsync(id);
        return result;
    }

    public async Task<IEnumerable<IRoom>> GetAll()
    {
        IEnumerable<Room> query = await _dbSet.ToListAsync();
        return query;
    }

    public void Add(IRoom entity)
    {
        _dbSet.Add((Room)entity);
    }

    public void Remove(IRoom entity)
    {
        _dbSet.Remove((Room)entity);
    }

    public void Update(IRoom room)
    {
        _dbSet.Update((Room)room);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }

    // this is used by team 2
    public IEnumerable<Room> GetRoomsRelatedToAccount(Guid accountId)
    {
        // load the data
        var allRooms = _db.Rooms.ToList();

        // filter the data
        var result = allRooms.Where(room => room.AccountId == accountId);
        return result;
    }
}