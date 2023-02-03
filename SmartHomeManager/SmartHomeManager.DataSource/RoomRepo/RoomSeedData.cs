﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.DataSource.RoomRepo;
public class RoomSeedData
{
    public static async Task Seed(ApplicationDbContext context)
    {
        // If there is data, don't do anything
        if (context.Accounts.Any()) return;
        if (context.Rooms.Any()) return;
        if (context.RoomCoordinates.Any()) return;
        if (context.Profiles.Any()) return;
        if (context.DeviceTypes.Any()) return;
        if (context.Devices.Any()) return;
        if (context.DeviceCoordinates.Any()) return;

        // Delete all existing database objects for Room domain
        context.Accounts.RemoveRange(context.Accounts);
        await context.SaveChangesAsync();

        context.Rooms.RemoveRange(context.Rooms);
        context.RoomCoordinates.RemoveRange(context.RoomCoordinates);
        context.Profiles.RemoveRange(context.Profiles);
        context.DeviceTypes.RemoveRange(context.DeviceTypes);
        context.Devices.RemoveRange(context.Devices);
        context.DeviceCoordinates.RemoveRange(context.DeviceCoordinates);
        await context.SaveChangesAsync();

        // create objects
        var accounts = new List<Account>
            {
                new Account
                {
                    AccountId = Guid.NewGuid(),
                    Email = "John",
                    Username = "Doe",
                    Address = "Ang Mo Kio",
                    Timezone = 8,
                    Password = "Ang Mo Kio",
                }
            };

        var rooms = new List<Room>
        {
            new Room
            {
                RoomId = Guid.NewGuid(),
                Name = "Bedroom",
                AccountId = accounts[0].AccountId,
            }
        };

        var roomCoordinates = new List<RoomCoordinate>
        {
            new RoomCoordinate
            {
                XCoordinate = 0,
                YCoordinate = 0,
                Width = 2,
                Height = 1,
                RoomId = rooms[0].RoomId,
            }
        };

        var profiles = new List<Profile>
        {
            new Profile
            {
                ProfileId = Guid.NewGuid(),
                Name = "My Profile",
                AccountId = accounts[0].AccountId,
            }
        };

        var deviceTypes = new List<DeviceType>
        {
            new DeviceType
            {
                DeviceTypeName = "Light",
            }
        };

        var devices = new List<Device>
        {
            new Device
            {
                DeviceId = Guid.NewGuid(),
                DeviceName = "Light",
                DeviceBrand = "Philips",
                DeviceModel = "Hue",
                DeviceTypeName = deviceTypes[0].DeviceTypeName,
                AccountId = accounts[0].AccountId,
                ProfileId = profiles[0].ProfileId,
            }
        };

        var deviceCoordinates = new List<DeviceCoordinate>
        {
            new DeviceCoordinate
            {
                XCoordinate = 0,
                YCoordinate = 0,
                Width = 2,
                Height = 1,
                DeviceId = devices[0].DeviceId,
            }
        };

        // add to repository and commit those changes
        // the order matters here because parent tables are created first
        await context.Accounts.AddRangeAsync(accounts);
        await context.SaveChangesAsync();

        await context.Rooms.AddRangeAsync(rooms);
        await context.SaveChangesAsync();

        await context.RoomCoordinates.AddRangeAsync(roomCoordinates);
        await context.SaveChangesAsync();

        await context.Profiles.AddRangeAsync(profiles);
        await context.SaveChangesAsync();

        await context.DeviceTypes.AddRangeAsync(deviceTypes);
        await context.SaveChangesAsync();

        await context.Devices.AddRangeAsync(devices);
        await context.SaveChangesAsync();

        await context.DeviceCoordinates.AddRangeAsync(deviceCoordinates);
        await context.SaveChangesAsync();
    }
}
