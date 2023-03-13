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

    public async Task Add(IRoom entity)
    {
        _dbSet.Add((Room)entity);
        await SaveChangesAsync();
    }

    public async Task Remove(IRoom entity)
    {
        _dbSet.Remove((Room)entity);
        await SaveChangesAsync();
    }

    public async Task Update(IRoom room)
    {
        _dbSet.Update((Room)room);
        await SaveChangesAsync();
    }

    // this is used by team 2
    public IEnumerable<IRoom> GetRoomsRelatedToAccount(Guid accountId)
    {
        // load the data
        var allRooms = _db.Rooms.ToList();

        // filter the data
        var result = allRooms.Where(room => room.AccountId == accountId);
        return result;
    }

    private async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}