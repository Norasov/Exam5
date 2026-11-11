using Application.DTOs.Patients;
using Application.HttpStatusCodes;

namespace Application.Interfaces;

public interface IPatientService
{
    Task<ServiceStatusCodes<bool>> AddPatientAsync(PatientDTO patientDTO);
    Task<ServiceStatusCodes<bool>> UpdatePatientAsync(PatientDTO patientDTO, int id);
    Task<ServiceStatusCodes<bool>> PatchPatientAsync(DeactivatePatientDTO patientDTO, int id);
    Task<ServiceStatusCodes<List<PatientDTO>>> GetPatientsAsync();
    Task<ServiceStatusCodes<PatientDTO>> GetPatientAsync(int id);
}
