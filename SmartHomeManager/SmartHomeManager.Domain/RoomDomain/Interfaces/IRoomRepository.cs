using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.Domain.RoomDomain.Interfaces;

public interface IRoomRepository
{
    Task<IRoom?> Get(Guid roomId);
    Task<IEnumerable<IRoom>> GetAll();
    Task Add(IRoom entity);
    Task Remove(IRoom entity);
    Task Update(IRoom entity);
    IEnumerable<IRoom> GetRoomsRelatedToAccount(Guid accountId);
}