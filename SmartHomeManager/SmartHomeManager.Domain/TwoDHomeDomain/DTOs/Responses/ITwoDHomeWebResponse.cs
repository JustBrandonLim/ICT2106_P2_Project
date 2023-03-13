using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;

public interface ITwoDHomeWebResponse
{
    public List<IRoomGrid> RoomGrids { get; set; }
}