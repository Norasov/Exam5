using Application.DTOs.Rooms;
using Application.HttpStatusCodes;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class RoomService(IRoomRepository repository) : IRoomService
{
    private readonly IRoomRepository _repository = repository;
    public async Task<ServiceStatusCodes<bool>> AddRoomAsync(RoomDTO roomDTO)
    {
        if (string.IsNullOrWhiteSpace(roomDTO.Name))
            return ServiceStatusCodes<bool>.BadRequest("Необходимо заполнить название палаты.");

        Room room = new()
        {
            Name = roomDTO.Name
        };

        var add = await _repository.AddRoomAsync(room);

        if (!add)
            return ServiceStatusCodes<bool>.BadRequest("Не удалосӣ добавить.");

        return ServiceStatusCodes<bool>.Created(add);
    }

    public async Task<ServiceStatusCodes<RoomDTO>> GetRoomAsync(int id)
    {
        var room = await _repository.GetRoomAsync(id);
        if (room is null)
            return ServiceStatusCodes<RoomDTO>.NotFound("Нет палаты с таким идентификатором.");

        RoomDTO roomDTO = new()
        {
            Name = room.Name,
            IsActive = room.IsActive
        };

        return ServiceStatusCodes<RoomDTO>.Ok(roomDTO);
    }


    public async Task<ServiceStatusCodes<List<RoomDTO>>> GetRoomsAsync()
    {
        var rooms = await _repository.GetRoomsAsync();
        List<RoomDTO> roomDTOs = [];
        foreach (var room in rooms)
        {
            RoomDTO roomDTO = new()
            {
                Name = room.Name,
                IsActive = room.IsActive
            };
            roomDTOs.Add(roomDTO);
        }

        return ServiceStatusCodes<List<RoomDTO>>.Ok(roomDTOs);
    }

    public async Task<ServiceStatusCodes<bool>> PatchRoomAsync(DeactivateRoomDTO roomDTO, int id)
    {
        var patch = await _repository.GetRoomAsync(id);

        if (patch is null)
            return ServiceStatusCodes<bool>.NotFound("Не палаты с таким идентификатором.");

        patch.IsActive = roomDTO.IsActive;

        var updated = await _repository.PatchRoomAsync(patch);

        if (!updated)
            return ServiceStatusCodes<bool>.BadRequest("Не удалось обновить.");

        return ServiceStatusCodes<bool>.Ok(true);
    }

    public async Task<ServiceStatusCodes<bool>> UpdateRoomAsync(RoomDTO roomDTO, int id)
    {
        var update = await _repository.GetRoomAsync(id);

        if (update is null)
            return ServiceStatusCodes<bool>.NotFound("Нет палаты с таким идентификатором.");

        if(string.IsNullOrWhiteSpace(roomDTO.Name))
            return ServiceStatusCodes<bool>.BadRequest("Необходимо заполнить название палаты.");

        update.Name = roomDTO.Name;

        var updated = await _repository.UpdateRoomAsync(update);

        if (!updated)
            return ServiceStatusCodes<bool>.BadRequest("Нет удалось обновить.");

        return ServiceStatusCodes<bool>.Ok(true);
    }
}
