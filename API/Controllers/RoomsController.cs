using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(IRoomService service) : ControllerBase
{
    private readonly IRoomService _service = service;

    [HttpGet]
    public async Task<ApiResponse<List<Room>>> GetRooms()
    {
        return await _service.GetRoomsAsync();
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> AddRoom(Room room)
    {
        return await _service.AddRoomAsync(room);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> UpdateRoom(Room room)
    {
        return await _service.UpdateRoomAsync(room);
    }

    [HttpPatch]
    public async Task<ApiResponse<bool>> PatchRoom(Room room)
    {
        return await _service.PatchRoomAsync(room);
    }
}
