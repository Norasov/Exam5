using System.Net;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class RoomService(IRoomRepository repository):IRoomService
{
    private readonly IRoomRepository _repository = repository;
    public async Task<ApiResponse<bool>> AddRoomAsync(Room room)
    {
        var add = await _repository.AddRoomAsync(room);
        return add == true
        ? new ApiResponse<bool>(HttpStatusCode.Created, "Added!", true)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not added!", false);
    }

    public async Task<ApiResponse<List<Room>>> GetRoomsAsync()
    {
        var rooms = await _repository.GetRoomsAsync();
        return new ApiResponse<List<Room>>(HttpStatusCode.OK, "information of rooms", rooms);
    }

    public async Task<ApiResponse<bool>> PatchRoomAsync(Room room)
    {
        var patch = await _repository.PatchRoomAsync(room);
        return patch == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Room updated!", patch)
        : new ApiResponse<bool>(HttpStatusCode.NotFound, "Room is not found!", false);

    }

    public async Task<ApiResponse<bool>> UpdateRoomAsync(Room room)
    {
        var update = await _repository.UpdateRoomAsync(room);
        return update == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, " Room updated!", update)
        : new ApiResponse<bool>(HttpStatusCode.NotFound, "Room is not found!", false);

    }

}
