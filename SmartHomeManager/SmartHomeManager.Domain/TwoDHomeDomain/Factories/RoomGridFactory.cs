using System.Diagnostics;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class RoomGridFactory
{
    // note, room must contain a RoomCoordinate reference and must not be null
    // you must have a room coordinate for a room in order to create a room grid
    public static RoomGrid CreateRoomGrid(IRoom room, Dictionary<Guid, bool> deviceStates)
    {
        Debug.Assert(room.RoomCoordinate != null, "room.RoomCoordinate != null");
        return new RoomGrid
        {
            RoomId = room.RoomId,
            RoomCoordinateId = room.RoomCoordinate.RoomCoordinateId,
            RoomName = room.Name,
            x = room.RoomCoordinate.XCoordinate,
            y = room.RoomCoordinate.YCoordinate,
            w = room.RoomCoordinate.Width,
            h = room.RoomCoordinate.Height,
            DeviceControls = DeviceControlFactory.CreateDeviceControl(room.Devices, deviceStates)
        };
    }
}