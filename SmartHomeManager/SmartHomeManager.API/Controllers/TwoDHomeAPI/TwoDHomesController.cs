using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Requests;
using SmartHomeManager.Domain.TwoDHomeDomain.DTOs.Responses;
using SmartHomeManager.Domain.TwoDHomeDomain.Interfaces;

namespace SmartHomeManager.API.Controllers.TwoDHomeAPI;

[Route("api/[controller]")]
[ApiController]
public class TwoDHomesController : ControllerBase
{
    private readonly ITwoDHomeReadService _twoDHomeReadService;
    private readonly ITwoDHomeWriteService _twoDHomeWriteService;

    public TwoDHomesController(ITwoDHomeReadService twoDHomeReadService, ITwoDHomeWriteService twoDHomeWriteService)
    {
        _twoDHomeReadService = twoDHomeReadService;
        _twoDHomeWriteService = twoDHomeWriteService;
    }

    [HttpGet("GetAllRoomGridsRelatedToAccount/{accountId}")]
    public ITwoDHomeWebResponse GetAllRoomGridsRelatedToAccount(Guid accountId)
    {
        return _twoDHomeReadService.GetAllRoomGridsRelatedToAccount(accountId);
    }

    [HttpPut("UpdateAllRoomGridsRelatedToAccount/{accountId}")]
    public IActionResult UpdateRoomGridsRelatedToAccount(Guid accountId, TwoDHomeWebRequest twoDHomeWebRequest)
    {
        var res = _twoDHomeWriteService.UpdateRoomGrids(accountId, twoDHomeWebRequest.RoomGrids);
        return Ok(res);
    }

    [HttpPut("ChangeDeviceState/{deviceId}")]
    public IActionResult UpdateRoomGridsRelatedToAccount(Guid deviceId, bool state)
    {
        var res = _twoDHomeWriteService.ChangeDeviceState(deviceId, state);
        return Ok(res);
    }

    [HttpPost("PostRoomCoordinate")]
    public async Task<ActionResult<IRoomCoordinateResponse>> PostRoomCoordinate(
        PostRoomCoordinateWebRequest roomCoordinateWebRequest)
    {
        var resp = await _twoDHomeWriteService.AddRoomCoordinate(
            roomCoordinateWebRequest.XCoordinate,
            roomCoordinateWebRequest.YCoordinate,
            roomCoordinateWebRequest.Width,
            roomCoordinateWebRequest.Height,
            roomCoordinateWebRequest.RoomId
        );
        if (resp == null) return BadRequest("RoomId not found or room already has a coordinate.");

        return CreatedAtAction("GetRoomCoordinate", new { roomCoordinateId = resp.RoomCoordinateId }, resp);
    }

    [HttpGet("GetRoomCoordinate/{roomCoordinateId}")]
    public async Task<ActionResult<IRoomCoordinateResponse>> GetRoomCoordinate(Guid roomCoordinateId)
    {
        var result = await _twoDHomeReadService.GetRoomCoordinate(roomCoordinateId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost("PostDeviceCoordinate")]
    public async Task<ActionResult<IDeviceCoordinateResponse>> PostDeviceCoordinate(
        PostDeviceCoordinateWebRequest deviceCoordinateWebRequest)
    {
        var resp = await _twoDHomeWriteService.AddDeviceCoordinate(
            deviceCoordinateWebRequest.XCoordinate,
            deviceCoordinateWebRequest.YCoordinate,
            deviceCoordinateWebRequest.Width,
            deviceCoordinateWebRequest.Height,
            deviceCoordinateWebRequest.DeviceId
        );
        if (resp == null) return BadRequest("DeviceId not found or device already has a coordinate.");

        return CreatedAtAction("GetDeviceCoordinate", new { deviceCoordinateId = resp.DeviceCoordinateId }, resp);
    }

    [HttpGet("GetDeviceCoordinate/{deviceCoordinateId}")]
    public async Task<ActionResult<IRoomCoordinateResponse>> GetDeviceCoordinate(Guid deviceCoordinateId)
    {
        var result = await _twoDHomeReadService.GetDeviceCoordinate(deviceCoordinateId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    // for testing purposes only
    [HttpGet("GetAllDeviceCoordinates")]
    public async Task<IActionResult> GetAllDeviceCoordinates()
    {
        var result = await _twoDHomeReadService.GetAllDeviceCoordinates();
        return Ok(result);
    }

    [HttpGet("GetAllRoomCoordinates")]
    public async Task<IActionResult> GetAllRoomCoordinates()
    {
        var result = await _twoDHomeReadService.GetAllRoomCoordinates();
        return Ok(result);
    }
}