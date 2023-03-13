﻿namespace SmartHomeManager.Domain.TwoDHomeDomain.Entities;

// using concrete classes for RoomGrid because ASP.Net does not support serialization of interface types,
// thus a concrete class is used instead.
// in addition, this is unlikely to change unless the client requirements change,
// thus we decided to use the concrete class instead

// likewise for DeviceControl, because it is used inside RoomGrid, it has to be concrete as well
public class RoomGrid
{
    public Guid RoomCoordinateId { get; set; }
    public Guid RoomId { get; set; }
    public string RoomName { get; set; }
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<DeviceControl> DeviceControls { get; set; }
}