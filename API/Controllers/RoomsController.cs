using API.Extensions;
using Application.DTOs.Rooms;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(IRoomService service) : ControllerBase
{
    private readonly IRoomService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _service.GetRoomsAsync();
        return this.ToActionResult(rooms);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoom(int id)
    {
        var room = await _service.GetRoomAsync(id);
        return this.ToActionResult(room);
    }

    [HttpPost]
    public async Task<IActionResult> AddRoom(RoomDTO roomDTO)
    {
        var add = await _service.AddRoomAsync(roomDTO);
        return this.ToActionResult(add);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRoom(RoomDTO roomDTO, int id)
    {
        var update = await _service.UpdateRoomAsync(roomDTO, id);
        return this.ToActionResult(update);
    }

    [HttpPatch("{id:int}/deactivate")]
    public async Task<IActionResult> PatchRoom(DeactivateRoomDTO roomDTO, int id)
    {
        var deactivate = await _service.PatchRoomAsync(roomDTO, id);
        return this.ToActionResult(deactivate);
    }
}
