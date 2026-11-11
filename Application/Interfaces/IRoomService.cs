using Application.DTOs.Rooms;
using Application.HttpStatusCodes;
using Domain.Models;

namespace Application.Interfaces;

public interface IRoomService
{
    Task<ServiceStatusCodes<bool>> AddRoomAsync(RoomDTO roomDTO);
    Task<ServiceStatusCodes<bool>> UpdateRoomAsync(RoomDTO roomDTO, int id);
    Task<ServiceStatusCodes<bool>> PatchRoomAsync(DeactivateRoomDTO roomDTO, int id);
    Task<ServiceStatusCodes<List<RoomDTO>>> GetRoomsAsync();
    Task<ServiceStatusCodes<RoomDTO>> GetRoomAsync(int id);
}
