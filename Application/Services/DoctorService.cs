using System.Net;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class DoctorService(IDoctorRepository repository) : IDoctorService
{
    private readonly IDoctorRepository _repository = repository;
    public async Task<ApiResponse<bool>> AddDoctorAsync(Doctor doctor)
    {
        var add = await _repository.AddDoctorAsync(doctor);
        return add == true
        ? new ApiResponse<bool>(HttpStatusCode.Created, "Added!", true)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not added!", false);
    }

    public async Task<ApiResponse<Doctor?>> GetDoctorAsync(int id)
    {
        var doctor = await _repository.GetDoctorAsync(id);
        return doctor != null
        ? new ApiResponse<Doctor?>(HttpStatusCode.OK, "Doctor with id", doctor)
        : new ApiResponse<Doctor?>(HttpStatusCode.NotFound, "Doctor is not found");
    }

    public async Task<ApiResponse<List<Doctor>>> GetDoctorsAsync()
    {
        var doctors = await _repository.GetDoctorsAsync();
        return new ApiResponse<List<Doctor>>(HttpStatusCode.OK, "information of doctors", doctors);
    }

    public async Task<ApiResponse<bool>> PatchDoctorAsync(Doctor doctor)
    {
        var patch = await _repository.PatchDoctorAsync(doctor);
        return patch == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Doctor deactivated!", patch)
        : new ApiResponse<bool>(HttpStatusCode.NotFound, "Doctor is not found!", false);

    }

    public async Task<ApiResponse<bool>> UpdateDoctorAsync(Doctor doctor)
    {
        var update = await _repository.UpdateDoctorAsync(doctor);
        return update == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, " Doctor updated!", update)
        : new ApiResponse<bool>(HttpStatusCode.NotFound, "Doctor is not found!", false);
    }
}
