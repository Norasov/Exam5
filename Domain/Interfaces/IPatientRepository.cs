using Domain.Models;

namespace Domain.Interfaces;

public interface IPatientRepository
{
    Task<bool> AddPatientAsync(Patient patient);
    Task<bool> UpdatePatientAsync(Patient patient);
    Task<bool> PatchPatientAsync(Patient patient);
    Task<List<Patient>> GetPatientsAsync();
    Task<Patient?> GetPatientAsync(int id);
}
