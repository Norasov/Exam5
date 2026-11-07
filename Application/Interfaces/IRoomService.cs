using Application.Responses;
using Domain.Models;

namespace Application.Interfaces;

public interface IRoomService
{
    Task<ApiResponse<bool>> AddRoomAsync(Room room);
    Task<ApiResponse<bool>> UpdateRoomAsync(Room room);
    Task<ApiResponse<bool>> PatchRoomAsync(Room room);
    Task<ApiResponse<List<Room>>> GetRoomsAsync();
}
