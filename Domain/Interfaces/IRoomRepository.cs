using Domain.Models;

namespace Domain.Interfaces;

public interface IRoomRepository
{
    Task<bool> AddRoomAsync(Room room);
    Task<bool> UpdateRoomAsync(Room room);
    Task<bool> PatchRoomAsync(Room room);
    Task<List<Room>> GetRoomsAsync();
    Task<Room?> GetRoomAsync(int id);
}
