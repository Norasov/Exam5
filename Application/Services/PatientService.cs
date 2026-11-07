using System.Net;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class PatientService(IPatientRepository repository) : IPatientService
{
    private readonly IPatientRepository _repository = repository;
    public async Task<ApiResponse<bool>> AddPatientAsync(Patient patient)
    {
        var add = await _repository.AddPatientAsync(patient);
        return add == true
        ? new ApiResponse<bool>(HttpStatusCode.Created, "Added!", true)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not added!", false);
    }

    public async Task<ApiResponse<Patient?>> GetPatientAsync(int id)
    {
        var patient = await _repository.GetPatientAsync(id);
        return patient != null
        ? new ApiResponse<Patient?>(HttpStatusCode.OK, "Patient with id", patient)
        : new ApiResponse<Patient?>(HttpStatusCode.NotFound, "Patient is not found");
    }

    public async Task<ApiResponse<List<Patient>>> GetPatientsAsync()
    {
        var patients = await _repository.GetPatientsAsync();
        return new ApiResponse<List<Patient>>(HttpStatusCode.OK, "information of patients", patients);
    }

    public async Task<ApiResponse<bool>> PatchPatientAsync(Patient patient)
    {
        var patch = await _repository.PatchPatientAsync(patient);
        return patch == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Patient updated!", patch)
        : new ApiResponse<bool>(HttpStatusCode.NotFound, "Patient is not found!", false);

    }

    public async Task<ApiResponse<bool>> UpdatePatientAsync(Patient patient)
    {
        var update = await _repository.UpdatePatientAsync(patient);
        return update == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, " Patient updated!", update)
        : new ApiResponse<bool>(HttpStatusCode.NotFound, "Patient is not found!", false);

    }
}
