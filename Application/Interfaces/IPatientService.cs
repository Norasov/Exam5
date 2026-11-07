using Application.Responses;
using Domain.Models;

namespace Application.Interfaces;

public interface IPatientService
{
    Task<ApiResponse<bool>> AddPatientAsync(Patient patient);
    Task<ApiResponse<bool>> UpdatePatientAsync(Patient patient);
    Task<ApiResponse<bool>> PatchPatientAsync(Patient patient);
    Task<ApiResponse<List<Patient>>> GetPatientsAsync();
    Task<ApiResponse<Patient?>> GetPatientAsync(int id);
}
