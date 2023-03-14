using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;

// Using concrete classes for RoomGrid because ASP.Net does not support serialization of interface types,
// thus a concrete class is used instead. This is because the RoomGrid is used as a request object in the controllers,
// which ASP.net uses for serialization. See TwoDHomeWebRequest.cs.
// In addition, this is unlikely to change unless the client requirements change,
// thus we decided to use the concrete class instead

// likewise for DeviceControl, because it is used inside RoomGrid, it has to be concrete as well
public class TwoDHomeWebResponse : ITwoDHomeWebResponse
{
    public List<RoomGrid> RoomGrids { get; set; }
}