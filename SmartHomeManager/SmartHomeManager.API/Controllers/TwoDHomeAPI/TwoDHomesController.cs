using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.RoomDomain.DTOs.Requests;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Services;
using SmartHomeManager.Domain.TwoDHomeDomain.Entities;
using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;
using SmartHomeManager.Domain.TwoDHomeDomain.Services;

namespace SmartHomeManager.API.Controllers.TwoDHomeAPI;

[Route("api/[controller]")]
[ApiController]
public class TwoDHomesController : ControllerBase
{
    private readonly TwoDHomeReadService _twoDHomeReadService;
    private readonly TwoDHomeWriteService _twoDHomeWriteService;

    public TwoDHomesController(ITwoDHomeRepository twoDHomeRepository)
    {
        _twoDHomeReadService = new TwoDHomeReadService(twoDHomeRepository);
        _twoDHomeWriteService = new TwoDHomeWriteService(twoDHomeRepository);
    }

    [HttpGet("GetDeviceCoordinate/{id}")]
    public async Task<DeviceCoordinate?> GetDeviceCoordinate(Guid id)
    {
        var result = await _twoDHomeReadService.GetDeviceCoordinate(id);
        return result;
    }

    [HttpGet("GetAllDeviceCoordinates")]
    public async Task<IEnumerable<DeviceCoordinate>> GetAllDeviceCoordinates()
    {
        var result = await _twoDHomeReadService.GetAllDeviceCoordinates();
        return result;
    }


    [HttpPut("UpdateDeviceCoordinate/{id}")]
    public async Task<IActionResult> UpdateDeviceCoordinate(Guid id, PutTwoDHomeWebRequest deviceCoordinate)
    {
        var res = await _twoDHomeReadService.GetDeviceCoordinate(id);

        if (res == null) return BadRequest();

        var xCoordinate = deviceCoordinate.XCoordinate;
        var yCoordinate = deviceCoordinate.YCoordinate;
        var height = deviceCoordinate.Height;
        var width = deviceCoordinate.Width;
        await _twoDHomeWriteService.UpdateDeviceCoordinate(id, xCoordinate, yCoordinate, height, width);

        return NoContent();
    }


    [HttpDelete("RemoveDeviceCoordinate/{id}")]
    public async Task<IActionResult> RemoveDeviceCoordinate(Guid id)
    {
        var res = await _twoDHomeReadService.GetDeviceCoordinate(id);
        if (res == null) return NotFound();
        await _twoDHomeWriteService.RemoveDeviceCoordinate(res.DeviceCoordinateId);

        return NoContent();
    }

    [HttpGet("GetRoomCoordinate/{id}")]
    public async Task<RoomCoordinate?> GetRoomCoordinate(Guid id)
    {
        var result = await _twoDHomeReadService.GetRoomCoordinate(id);
        return result;
    }

    [HttpGet("GetAllRoomCoordinates")]
    public async Task<IEnumerable<RoomCoordinate>> GetAllRoomCoordinates()
    {
        var result = await _twoDHomeReadService.GetAllRoomCoordinates();
        return result;
    }

    [HttpPut("UpdateRoomCoordinate/{id}")]
    public async Task<IActionResult> UpdateRoomCoordinate(Guid id, PutTwoDHomeWebRequest roomCoordinate)
    {
        var res = await _twoDHomeReadService.GetRoomCoordinate(id);

        if (res == null) return BadRequest();

        var xCoordinate = roomCoordinate.XCoordinate;
        var yCoordinate = roomCoordinate.YCoordinate;
        var height = roomCoordinate.Height;
        var width = roomCoordinate.Width;
        var updateResult =
            await _twoDHomeWriteService.UpdateRoomCoordinate(id, xCoordinate, yCoordinate, height, width);

        if (!updateResult) return BadRequest("Collision detected");

        return NoContent();
    }

    [HttpDelete("RemoveRoomCoordinate/{id}")]
    public async Task<IActionResult> RemoveRoomCoordinate(Guid id)
    {
        var res = await _twoDHomeReadService.GetRoomCoordinate(id);
        if (res == null) return NotFound();
        await _twoDHomeWriteService.RemoveRoomCoordinate(res.RoomCoordinateId);

        return NoContent();
    }
}