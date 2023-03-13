using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;

public class TwoDHomeWebResponse : ITwoDHomeWebResponse
{
    public List<RoomGrid> RoomGrids { get; set; }
}