using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class DoctorRepository(AppDbContext context) : IDoctorRepository
{
    private readonly AppDbContext _context = context;
    public async Task<bool> AddDoctorAsync(Doctor doctor)
    {
        using var connection = _context.Connection();
        var query = @"insert into doctors(fullname, specialty, isactive, hiredat) values
        (@fullname, @specialty, @isactive, @hiredat)";
        var addDoctor = await connection.ExecuteAsync(query, doctor);
        return addDoctor > 0;
    }

    public async Task<Doctor?> GetDoctorAsync(int id)
    {
        using var connection = _context.Connection();
        var query = "select * from doctors where id = @id";
        return await connection.QueryFirstOrDefaultAsync<Doctor>(query, new { id });
    }

    public async Task<List<Doctor>> GetDoctorsAsync()
    {
        using var connection = _context.Connection();
        var query = "select * from doctors";
        var doctors = await connection.QueryAsync<Doctor>(query);
        return doctors.ToList();
    }

    public async Task<bool> PatchDoctorAsync(Doctor doctor)
    {
        using var connection = _context.Connection();
        var query = @"update doctors set isactive = false where id = @id";

        var patch = await connection.ExecuteAsync(query, doctor);
        return patch > 0;
    }

    public async Task<bool> UpdateDoctorAsync(Doctor doctor)
    {
        using var connection = _context.Connection();
        var query = @"update doctors set
        fullname = @fullname,
        specialty = @specialty,
        isactive = @isactive,
        hiredat = @hiredat
        where id = @id";

        var update = await connection.ExecuteAsync(query, doctor);
        return update > 0;
    }
}
