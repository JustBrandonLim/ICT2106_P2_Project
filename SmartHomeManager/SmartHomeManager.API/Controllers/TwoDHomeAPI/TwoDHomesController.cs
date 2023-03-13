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

    // for testing purposes
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