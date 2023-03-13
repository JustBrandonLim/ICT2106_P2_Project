using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;

namespace SmartHomeManager.Domain.TwoDHomeDomain.Factories;

public class RoomGridFactory
{
    public static IRoomGrid CreateRoomGrid(Room room, Dictionary<Guid, bool> deviceStates)
    {
        return new RoomGrid
        {
            RoomId = room.RoomId,
            RoomCoordinateId = room.RoomCoordinate.RoomCoordinateId,
            RoomName = room.Name,
            XCoordinate = room.RoomCoordinate.XCoordinate,
            YCoordinate = room.RoomCoordinate.YCoordinate,
            Width = room.RoomCoordinate.Width,
            Height = room.RoomCoordinate.Height,
            DeviceControls = DeviceControlFactory.CreateDeviceControl(room.Devices, deviceStates)
        };
    }
}