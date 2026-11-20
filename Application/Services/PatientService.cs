using Application.DTOs.Patients;
using Application.HttpStatusCodes;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class PatientService(IPatientRepository repository) : IPatientService
{
    private readonly IPatientRepository _repository = repository;
    public async Task<ServiceStatusCodes<bool>> AddPatientAsync(PatientDTO patientDTO)
    {
        if (string.IsNullOrWhiteSpace(patientDTO.FullName))
            return ServiceStatusCodes<bool>.BadRequest("Вам необходимо написать полное имя пациена.");

        if (string.IsNullOrWhiteSpace(patientDTO.Phone))
            return ServiceStatusCodes<bool>.BadRequest("Вам необходимо заполнить номер телефона.");

        if (await _repository.PhoneExistsAsync(patientDTO.Phone))
            return ServiceStatusCodes<bool>.Conflict("Пациент c таким номером телефона уже существует.");

        Patient patient = new()
        {
            FullName = patientDTO.FullName,
            Phone = patientDTO.Phone,
            BirthDate = patientDTO.BirthDate
        };
        var add = await _repository.AddPatientAsync(patient);

        if (!add)
            return ServiceStatusCodes<bool>.BadRequest("Не удалось добавить.");

        return ServiceStatusCodes<bool>.Created(add);
    }

    public async Task<ServiceStatusCodes<PatientDTO>> GetPatientAsync(int id)
    {
        var patient = await _repository.GetPatientAsync(id);
        if (patient is null)
            return ServiceStatusCodes<PatientDTO>.NotFound("Нет пациента с таким идентификатором.");

        PatientDTO patientDTO = new()
        {
            FullName = patient.FullName,
            Phone = patient.Phone,
            BirthDate = patient.BirthDate,
            IsActive = patient.IsActive
        };

        return ServiceStatusCodes<PatientDTO>.Ok(patientDTO);
    }

    public async Task<ServiceStatusCodes<List<PatientDTO>>> GetPatientsAsync()
    {
        var patients = await _repository.GetPatientsAsync();
        List<PatientDTO> patientDTOs = [];
        foreach (var patient in patients)
        {
            PatientDTO patientDTO = new()
            {
                FullName = patient.FullName,
                Phone = patient.Phone,
                BirthDate = patient.BirthDate,
                IsActive = patient.IsActive
            };
            patientDTOs.Add(patientDTO);
        }

        return ServiceStatusCodes<List<PatientDTO>>.Ok(patientDTOs);
    }

    public async Task<ServiceStatusCodes<bool>> PatchPatientAsync(DeactivatePatientDTO patientDTO, int id)
    {
        var patch = await _repository.GetPatientAsync(id);
        if (patch is null)
            return ServiceStatusCodes<bool>.NotFound("Нет пациента с таким идентификатором.");

        patch.IsActive = patientDTO.IsActive;

        var updated = await _repository.PatchPatientAsync(patch);
        if (!updated)
            return ServiceStatusCodes<bool>.BadRequest("Не удалось обновить.");

        return ServiceStatusCodes<bool>.Ok(updated);
    }

    public async Task<ServiceStatusCodes<bool>> UpdatePatientAsync(PatientDTO patientDTO, int id)
    {
        var update = await _repository.GetPatientAsync(id);

        if (update is null)
            return ServiceStatusCodes<bool>.NotFound("Нет пациента с таким идентификатором.");


        if (string.IsNullOrWhiteSpace(patientDTO.FullName))
            return ServiceStatusCodes<bool>.BadRequest("Вам необходимо написать полное имя пациента.");

        if (string.IsNullOrWhiteSpace(patientDTO.Phone))
            return ServiceStatusCodes<bool>.BadRequest("Вам необходимо заполнить номер телефона.");

        if (await _repository.PhoneExistsAsync(patientDTO.Phone, id))
            return ServiceStatusCodes<bool>.Conflict("Пациент c таким номером телефона уже существует.");

        update.FullName = patientDTO.FullName;
        update.Phone = patientDTO.Phone;
        update.BirthDate = patientDTO.BirthDate;

        var updated = await _repository.UpdatePatientAsync(update);

        if (!updated)
            return ServiceStatusCodes<bool>.BadRequest("Не удалось обновить.");

        return ServiceStatusCodes<bool>.Ok(updated);
    }
}
