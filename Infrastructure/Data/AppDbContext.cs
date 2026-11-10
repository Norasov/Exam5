using System.Data;
using Dapper;
using Npgsql;

namespace Infrastructure.Data;

public class AppDbContext
{
    private readonly string connectionString = "Host=localhost;Port=5432;Database=ClinicQueueDb;Username=postgres;Password=p000";

    static AppDbContext()
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
    }

    public IDbConnection Connection() => new NpgsqlConnection(connectionString);
}
