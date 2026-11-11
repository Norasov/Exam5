using Application.DTOs.Doctors;
using Application.HttpStatusCodes;
using Domain.Models;

namespace Application.Interfaces;

public interface IDoctorService
{
    Task<ServiceStatusCodes<bool>> AddDoctorAsync(DoctorDTO doctorDTO);
    Task<ServiceStatusCodes<bool>> UpdateDoctorAsync(DoctorDTO doctorDTO, int id);
    Task<ServiceStatusCodes<bool>> PatchDoctorAsync(DeactivateDoctorDTO deactivateDoctorDTO, int id);
    Task<ServiceStatusCodes<List<DoctorDTO>>> GetDoctorsAsync();
    Task<ServiceStatusCodes<DoctorDTO>> GetDoctorAsync(int id);

}
