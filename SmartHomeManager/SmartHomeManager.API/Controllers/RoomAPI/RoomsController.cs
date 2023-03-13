using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.DTOs.Requests;
using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;
using SmartHomeManager.Domain.RoomDomain.Interfaces;

namespace SmartHomeManager.API.Controllers.RoomAPI;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly IRoomReadService _roomReadService;
    private readonly IRoomWriteService _roomWriteService;

    public RoomsController(IRoomWriteService roomWriteService, IRoomReadService roomReadService)
    {
        _roomWriteService = roomWriteService;
        _roomReadService = roomReadService;
    }

    // GET: api/Rooms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IRoomWebResponse>>> GetRooms()
    {
        return Ok(await _roomReadService.GetAllRooms());
    }

    // GET: api/Rooms/5
    [HttpGet("{roomId}")]
    public async Task<ActionResult<IRoomWebResponse>> GetRoom(Guid roomId)
    {
        var result = await _roomReadService.GetRoomById(roomId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    // PUT: api/Rooms/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{roomId}")]
    public async Task<IActionResult> PutRoom(Guid roomId, PutRoomWebRequest roomWebRequest)
    {
        var res = await _roomReadService.GetRoomById(roomId);
        if (res == null) return BadRequest();
        var name = roomWebRequest.Name ?? res.Name;
        await _roomWriteService.UpdateRoom(roomId, name);
        return Ok();
    }

    // POST: api/Rooms
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<IRoomWebResponse>> PostRoom(PostRoomWebRequest roomWebRequest)
    {
        var resp = await _roomWriteService.AddRoom(roomWebRequest.Name, roomWebRequest.AccountId);

        // routeValues specifies the action to be called and the route values to be used for that action
        // for example new { roomId = xxx } must match [HttpGet("{roomId}")]
        // the parameter names must match, roomId with roomId
        return CreatedAtAction("GetRoom", new { roomId = resp.RoomId }, resp);
    }

    // DELETE: api/Rooms/5
    [HttpDelete("{roomId}")]
    public async Task<IActionResult> DeleteRoom(Guid roomId)
    {
        var res = await _roomReadService.GetRoomById(roomId);
        if (res == null) return NotFound();
        await _roomWriteService.RemoveRoom(res.RoomId);
        return Ok();
    }

    // GET: api/Rooms/GetRoomsRelatedToAccount/accountId
    [HttpGet("GetRoomsRelatedToAccount/{accountId}")]
    public ActionResult<IEnumerable<IRoomWebResponse>> GetRoomsRelatedToAccount(Guid accountId)
    {
        var result = _roomReadService.GetRoomsRelatedToAccount(accountId);
        return Ok(result);
    }

    // For testing purposes only
    // GET: api/Rooms/GetDevicesRelatedToRoom/roomId
    [HttpGet("GetDevicesInRoom/{roomId}")]
    public ActionResult<IEnumerable<Device>> GetDevicesInRoom(Guid roomId)
    {
        var result = _roomReadService.GetDevicesInRoom(roomId);
        if (!result.Any()) return NotFound();
        return Ok(result);
    }
}