using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.DTOs.Requests;
using SmartHomeManager.Domain.RoomDomain.DTOs.Responses;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;
using SmartHomeManager.Domain.RoomDomain.Services;

namespace SmartHomeManager.API.Controllers.RoomAPI;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly RoomReadService _roomReadService;
    private readonly RoomWriteService _roomWriteService;

    public RoomsController(IRoomRepository roomRepository, IDeviceInformationServiceMock deviceInformationService)
    {
        _roomReadService = new RoomReadService(roomRepository, deviceInformationService);
        _roomWriteService = new RoomWriteService(roomRepository);
    }

    // GET: api/Rooms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetRoomWebResponse>>> GetRooms()
    {
        return Ok(await _roomReadService.GetAllRooms());
    }

    // GET: api/Rooms/5
    [HttpGet("{roomId}")]
    public async Task<ActionResult<GetRoomWebResponse>> GetRoom(Guid roomId)
    {
        var result = await _roomReadService.GetRoomById(roomId);
        if (result == null) return NotFound();
        return result;
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

        return NoContent();
    }

    // POST: api/Rooms
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GetRoomWebResponse>> PostRoom(PostRoomWebRequest roomWebRequest)
    {
        if (roomWebRequest.AccountId == Guid.Empty) return BadRequest("AccountId cannot be empty");

        var resp = await _roomWriteService.AddRoom(roomWebRequest.Name, roomWebRequest.AccountId);

        // the route values specifies the action to be called and the route values to be used for that action
        // for example new { roomId = xxx } must match [HttpGet("{roomId}")]
        // as in the parameter names must match, roomId with roomId
        return CreatedAtAction("GetRoom", new { roomId = resp.RoomId }, resp);
    }

    // DELETE: api/Rooms/5
    [HttpDelete("{roomId}")]
    public async Task<IActionResult> DeleteRoom(Guid roomId)
    {
        var res = await _roomReadService.GetRoomById(roomId);
        if (res == null) return NotFound();

        await _roomWriteService.RemoveRoom(res.RoomId);

        return NoContent();
    }

    // GET: api/Rooms/GetRoomsRelatedToAccount/accountId
    [HttpGet("GetRoomsRelatedToAccount/{accountId}")]
    public ActionResult<IEnumerable<GetRoomWebResponse>> GetRoomsRelatedToAccount(Guid accountId)
    {
        var result = _roomReadService.GetRoomsRelatedToAccount(accountId);
        return Ok(result);
    }

    // GET: api/Rooms/GetDevicesRelatedToRoom/roomId
    [HttpGet("GetDevicesInRoom/{roomId}")]
    public ActionResult<IEnumerable<Device>> GetDevicesInRoom(Guid roomId)
    {
        var result = _roomReadService.GetDevicesInRoom(roomId);
        if (!result.Any()) return NotFound();

        return Ok(result);
    }
}