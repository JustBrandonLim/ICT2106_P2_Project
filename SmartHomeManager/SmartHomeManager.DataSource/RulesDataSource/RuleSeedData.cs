using System;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;


namespace SmartHomeManager.DataSource.RulesDataSource;

public class RuleSeedData
{
    public static async Task Seed(ApplicationDbContext context)
    {
        // If there is data, don't do anything
        //if (context.Accounts.Any()) return;
        //if (context.Rooms.Any()) return;
        //if (context.RoomCoordinates.Any()) return;
        //if (context.DeviceTypes.Any()) return;
        //if (context.Devices.Any()) return;
        //if (context.DeviceCoordinates.Any()) return;

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
            new()
            {
                AccountId = Guid.NewGuid(),
                Email = "John",
                Username = "Doe",
                Address = "Ang Mo Kio",
                Timezone = 8,
                Password = "Ang Mo Kio"
            }
        };

        var profiles = new List<Profile>
        {
            new()
            {
                ProfileId = Guid.NewGuid(),
                Name = "My Profile",
                AccountId = accounts[0].AccountId
            }
        };

        var scenario = new List<Scenario>
        {
            new()
            {
                ScenarioId = new Guid("AC38AF14-9A57-4DF3-89F3-78F9CE9F4983"),
                ScenarioName = "string",
                ProfileId = profiles[0].ProfileId
            }
        };

        var deviceTypes = new List<DeviceType>
        {
            new()
            {
                DeviceTypeName = "Light"
            }
        };

        var devices = new List<Device>
        {
            new()
            {
                DeviceId = new Guid("5CDDF6A7-C3B8-47A7-9DA1-19E1795EBF69"),
                DeviceName = "Light",
                DeviceBrand = "Philips",
                DeviceModel = "Hue",
                DeviceTypeName = deviceTypes[0].DeviceTypeName,
                AccountId = accounts[0].AccountId
            }
        };

        var rooms = new List<Room>
        {
            new()
            {
                RoomId = Guid.NewGuid(),
                Name = "Bedroom",
                AccountId = accounts[0].AccountId
            }
        };

        var roomCoordinates = new List<RoomCoordinate>
        {
            new()
            {
                XCoordinate = 0,
                YCoordinate = 0,
                Width = 2,
                Height = 1,
                RoomId = rooms[0].RoomId
            }
        };

        var deviceCoordinates = new List<DeviceCoordinate>
        {
            new()
            {
                XCoordinate = 0,
                YCoordinate = 0,
                Width = 2,
                Height = 1,
                DeviceId = devices[0].DeviceId
            }
        };

        var rule = new Rule
        {
            RuleId = Guid.NewGuid(),
            ScenarioId = scenario[0].ScenarioId,
            ConfigurationValue = 0,
            ActionTrigger = "string",
            RuleName = "string",
            StartTime = Convert.ToDateTime("2023-02-04T07:21:26.934Z"),
            EndTime = Convert.ToDateTime("2023-02-04T07:21:26.934Z"),
            DeviceId = devices[0].DeviceId
        };

        // add to repository and commit those changes
        // the order matters here because parent tables are created first
        await context.Accounts.AddRangeAsync(accounts);
        await context.SaveChangesAsync();

        await context.Profiles.AddRangeAsync(profiles);
        await context.SaveChangesAsync();

        await context.Scenarios.AddRangeAsync(scenario);
        await context.SaveChangesAsync();

        await context.DeviceTypes.AddRangeAsync(deviceTypes);
        await context.SaveChangesAsync();

        await context.Devices.AddRangeAsync(devices);
        await context.SaveChangesAsync();

        await context.Rooms.AddRangeAsync(rooms);
        await context.SaveChangesAsync();

        await context.RoomCoordinates.AddRangeAsync(roomCoordinates);
        await context.SaveChangesAsync();

        await context.DeviceCoordinates.AddRangeAsync(deviceCoordinates);
        await context.SaveChangesAsync();

        //await context.Rules.AddRangeAsync(rule);
        //await context.SaveChangesAsync();

        //test
    }
}