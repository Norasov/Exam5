using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DoctorRepository(AppDbContext context) : IDoctorRepository
{
    private readonly AppDbContext _context = context;
    public async Task<bool> AddDoctorAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        var create = await _context.SaveChangesAsync();
        return create > 0;
    }

    public async Task<Doctor?> GetDoctorAsync(int id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    public async Task<List<Doctor>> GetDoctorsAsync()
    {
        return await _context.Doctors.AsNoTracking().ToListAsync();
    }

    public async Task<bool> PatchDoctorAsync(Doctor doctor)
    {
        _context.Entry(doctor).Property(d => d.IsActive).IsModified = true;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateDoctorAsync(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        return await _context.SaveChangesAsync() > 0;
    }
}
