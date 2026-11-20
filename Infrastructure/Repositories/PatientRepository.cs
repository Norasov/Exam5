using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PatientRepository(AppDbContext context) : IPatientRepository
{
    private readonly AppDbContext _context = context;
    public async Task<bool> AddPatientAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Patient?> GetPatientAsync(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task<List<Patient>> GetPatientsAsync()
    {
        return await _context.Patients.AsNoTracking().ToListAsync();
    }

    public async Task<bool> PatchPatientAsync(Patient patient)
    {
        _context.Entry(patient).Property(p => p.IsActive).IsModified = true;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> PhoneExistsAsync(string phone, int? excludeId = null)
    {
        return await _context.Patients
        .AnyAsync(p => p.Phone == phone && (excludeId == null || p.Id != excludeId));
    }

    public async Task<bool> UpdatePatientAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        return await _context.SaveChangesAsync() > 0;
    }
}