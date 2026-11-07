using Application.Responses;
using Domain.Models;

namespace Application.Interfaces;

public interface IDoctorService
{
    Task<ApiResponse<bool>> AddDoctorAsync(Doctor doctor);
    Task<ApiResponse<bool>> UpdateDoctorAsync(Doctor doctor);
    Task<ApiResponse<bool>> PatchDoctorAsync(Doctor doctor);
    Task<ApiResponse<List<Doctor>>> GetDoctorsAsync();
    Task<ApiResponse<Doctor?>> GetDoctorAsync(int id);

}
