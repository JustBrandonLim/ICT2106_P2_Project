using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Requests;

public class TwoDHomeWebRequest
{
    public List<RoomGrid> RoomGrids { get; set; }
}