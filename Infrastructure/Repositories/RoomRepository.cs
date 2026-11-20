using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoomRepository(AppDbContext context):IRoomRepository
{
    private readonly AppDbContext _context = context;
    public async Task<bool> AddRoomAsync(Room room)
    {
        _context.Rooms.Add(room);
        return await _context.SaveChangesAsync()>0;
    }

    public async Task<Room?> GetRoomAsync(int id)
    {
        return await _context.Rooms.FindAsync(id);
    }


    public async Task<List<Room>> GetRoomsAsync()
    {
        return await _context.Rooms.AsNoTracking().ToListAsync();
    }

    public async Task<bool> PatchRoomAsync(Room room)
    {
        _context.Entry(room).Property(r => r.IsActive).IsModified = true;
        return await _context.SaveChangesAsync()>0;
    }

    public async Task<bool> UpdateRoomAsync(Room room)
    {
        _context.Rooms.Update(room);
        return await _context.SaveChangesAsync()>0;
    }

}