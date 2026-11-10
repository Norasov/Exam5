using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class PatientRepository(AppDbContext context) : IPatientRepository
{
    private readonly AppDbContext _context = context;
    public async Task<bool> AddPatientAsync(Patient patient)
    {
        using var connection = _context.Connection();
        var query = @"insert into patients(fullname, phone, birthdate, isactive) values
        (@fullname, @phone, @birthdate, @isactive)";
        try
        {
            var addPatient = await connection.ExecuteAsync(query, patient);
            return addPatient > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
        {
            return false;
        }
    }

    public async Task<Patient?> GetPatientAsync(int id)
    {
        using var connection = _context.Connection();
        var query = "select * from patients where id = @id";
        return await connection.QueryFirstOrDefaultAsync<Patient>(query, new { id });
    }

    public async Task<List<Patient>> GetPatientsAsync()
    {
        using var connection = _context.Connection();
        var query = "select * from patients";
        var patients = await connection.QueryAsync<Patient>(query);
        return patients.ToList();
    }

    public async Task<bool> PatchPatientAsync(Patient patient)
    {
        using var connection = _context.Connection();
        var query = @"update patients set isactive = false where id = @id";

        var patch = await connection.ExecuteAsync(query, patient);
        return patch > 0;
    }

    public async Task<bool> UpdatePatientAsync(Patient patient)
    {
        using var connection = _context.Connection();
        var query = @"update patients set
        fullname = @fullname,
        phone = @phone,
        birthdate = @birthdate,
        isactive = @isactive,
        createdat = @createdat
        where id = @id";

        var update = await connection.ExecuteAsync(query, patient);
        return update > 0;
    }
}