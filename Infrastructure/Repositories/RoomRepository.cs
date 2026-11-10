using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class RoomRepository(AppDbContext context):IRoomRepository
{
    private readonly AppDbContext _context = context;
    public async Task<bool> AddRoomAsync(Room room)
    {
        using var connection = _context.Connection();
        var query = @"insert into rooms(name, isactive) values
        (@name, @isactive)";
        var addRoom = await connection.ExecuteAsync(query, room);
        return addRoom > 0;
    }

    public async Task<List<Room>> GetRoomsAsync()
    {
        using var connection = _context.Connection();
        var query = "select * from rooms";
        var rooms = await connection.QueryAsync<Room>(query);
        return rooms.ToList();
    }

    public async Task<bool> PatchRoomAsync(Room room)
    {
        using var connection = _context.Connection();
        var query = @"update rooms set isactive = false
        where id = @id";

        var patch = await connection.ExecuteAsync(query, room);
        return patch > 0;
    }

    public async Task<bool> UpdateRoomAsync(Room room)
    {
        using var connection = _context.Connection();
        var query = @"update rooms set
        name = @name,
        isactive = @isactive
        where id = @id";

        var update = await connection.ExecuteAsync(query, room);
        return update > 0;
    }

}