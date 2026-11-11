using System.Net;
using Application.DTOs.Doctors;
using Application.HttpStatusCodes;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class DoctorService(IDoctorRepository repository) : IDoctorService
{
    private readonly IDoctorRepository _repository = repository;
    public async Task<ServiceStatusCodes<bool>> AddDoctorAsync(DoctorDTO doctorDTO)
    {
        if (string.IsNullOrWhiteSpace(doctorDTO.FullName))
            return ServiceStatusCodes<bool>.BadRequest("Необходимо заполнить полное имя.");

        if (string.IsNullOrWhiteSpace(doctorDTO.Specialty))
            return ServiceStatusCodes<bool>.BadRequest("Необходимо заполнить специальность.");
        
        Doctor doctor = new()
        {
            FullName = doctorDTO.FullName,
            Specialty = doctorDTO.Specialty,
            HireDat = doctorDTO.HireDat
        };

        var add = await _repository.AddDoctorAsync(doctor);
        if(!add)
            return ServiceStatusCodes<bool>.BadRequest("Не удалось добавить.");

        return ServiceStatusCodes<bool>.Created(add);
    }

    public async Task<ServiceStatusCodes<DoctorDTO>> GetDoctorAsync(int id)
    {
        var doctor = await _repository.GetDoctorAsync(id);
        if (doctor is null)
            return ServiceStatusCodes<DoctorDTO>.NotFound("нет доктора с таким идентификатором.");

        DoctorDTO doctorDTO = new()
        {
            FullName = doctor.FullName,
            Specialty = doctor.Specialty,
            HireDat = doctor.HireDat,
            IsActive = doctor.IsActive
        };

        return ServiceStatusCodes<DoctorDTO>.Ok(doctorDTO);
    }

    public async Task<ServiceStatusCodes<List<DoctorDTO>>> GetDoctorsAsync()
    {
        var doctors = await _repository.GetDoctorsAsync();
        List<DoctorDTO> doctorDTOs = [];
        foreach (var doctor in doctors)
        {
            DoctorDTO doctorDTO = new();
            doctorDTO.FullName = doctor.FullName;
            doctorDTO.Specialty = doctor.Specialty;
            doctorDTO.HireDat = doctor.HireDat;
            doctorDTO.IsActive = doctor.IsActive;
            doctorDTOs.Add(doctorDTO);
        }

        return ServiceStatusCodes<List<DoctorDTO>>.Ok(doctorDTOs);
    }

    public async Task<ServiceStatusCodes<bool>> PatchDoctorAsync(DeactivateDoctorDTO deactivateDoctorDTO, int id)
    {
        var patch = await _repository.GetDoctorAsync(id);
        if (patch is null)
            return ServiceStatusCodes<bool>.NotFound("Нет доктора с таким идентификатором.");

        patch.IsActive = deactivateDoctorDTO.IsActive;

        var updated = await _repository.PatchDoctorAsync(patch);

        if (!updated)
            return ServiceStatusCodes<bool>.BadRequest("Ну удалось обновить.");

        return ServiceStatusCodes<bool>.Ok(true);
    }

    public async Task<ServiceStatusCodes<bool>> UpdateDoctorAsync(DoctorDTO doctorDTO, int id)
    {
        var update = await _repository.GetDoctorAsync(id);
        if (update is null)
            return ServiceStatusCodes<bool>.NotFound("Нет доктора с таким идентификатором.");

        if (string.IsNullOrWhiteSpace(doctorDTO.FullName))
            return ServiceStatusCodes<bool>.BadRequest("Необходимо заполнить полное имя.");

        if (string.IsNullOrWhiteSpace(doctorDTO.Specialty))
            return ServiceStatusCodes<bool>.BadRequest("Необходимо заполнить специальность.");

        update.FullName = doctorDTO.FullName;
        update.Specialty = doctorDTO.Specialty;
        update.HireDat = doctorDTO.HireDat;

        var updated = await _repository.UpdateDoctorAsync(update);
        if (!updated)
            return ServiceStatusCodes<bool>.BadRequest("Не удалось обновить.");

        return ServiceStatusCodes<bool>.Ok(true);
    }
}
