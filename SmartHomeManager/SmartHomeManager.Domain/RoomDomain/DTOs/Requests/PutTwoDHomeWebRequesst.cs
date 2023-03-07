namespace SmartHomeManager.Domain.RoomDomain.DTOs.Requests;

public class PutTwoDHomeWebRequest
{
    public int XCoordinate { get; set; }
    
    public int YCoordinate { get; set; }
    
    public int Height { get; set; }
    
    public int Width { get; set; }
}