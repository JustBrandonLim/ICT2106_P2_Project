using System.Diagnostics;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class RoomGridFactory
{
    // const variables are implicitly static in C#
    private const int EmptyCoordinate = -1;

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

    public static RoomGrid CreateEmptyRoomGrid(IRoom room)
    {
        return new RoomGrid
        {
            RoomId = room.RoomId,
            RoomCoordinateId = Guid.Empty,
            RoomName = room.Name,
            x = EmptyCoordinate,
            y = EmptyCoordinate,
            w = EmptyCoordinate,
            h = EmptyCoordinate,
            DeviceControls = new List<DeviceControl>()
        };
    }
}