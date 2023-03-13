using System.Linq.Expressions;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Interfaces;

public interface IRoomRepository
{
    Task<IRoom?> Get(Guid roomId);
    Task<IEnumerable<IRoom>> GetAll();
    void Add(IRoom entity);
    void Remove(IRoom entity);
    void Update(IRoom entity);
    Task SaveChangesAsync();
    IEnumerable<Room> GetRoomsRelatedToAccount(Guid accountId);
}