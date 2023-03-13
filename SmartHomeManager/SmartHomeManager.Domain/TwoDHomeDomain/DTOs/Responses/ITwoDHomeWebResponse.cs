using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;

public interface ITwoDHomeWebResponse
{
    public List<RoomGrid> RoomGrids { get; set; }
}