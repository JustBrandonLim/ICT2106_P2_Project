namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

// Using concrete classes for RoomGrid because ASP.Net does not support serialization of interface types,
// thus a concrete class is used instead. This is because the RoomGrid is used as a request object in the controllers,
// which ASP.net uses for serialization. See TwoDHomeWebRequest.cs.
// In addition, this is unlikely to change unless the client requirements change,
// thus we decided to use the concrete class instead

// likewise for DeviceControl, because it is used inside RoomGrid, it has to be concrete as well
public class RoomGrid
{
    public Guid RoomCoordinateId { get; set; }
    public Guid RoomId { get; set; }
    public string RoomName { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int w { get; set; }
    public int h { get; set; }
    public List<DeviceControl> DeviceControls { get; set; }
}