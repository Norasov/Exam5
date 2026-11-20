using Domain.Models;

namespace Domain.Interfaces;

public interface IDoctorRepository
{
    Task<bool> AddDoctorAsync(Doctor doctor);
    Task<bool> UpdateDoctorAsync(Doctor doctor);
    Task<bool> PatchDoctorAsync(Doctor doctor);
    Task<List<Doctor>> GetDoctorsAsync();
    Task<Doctor?> GetDoctorAsync(int id);
}
