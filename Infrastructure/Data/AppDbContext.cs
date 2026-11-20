using System.Data;
using Dapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    static AppDbContext()
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public IDbConnection Connection() => new NpgsqlConnection(Database.GetConnectionString());


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName()!.ToLower());
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToLower());
            }
        }

        modelBuilder.Entity<Patient>().HasIndex(x=>x.Phone).IsUnique();

        modelBuilder.Entity<Doctor>().Property(x => x.IsActive).HasDefaultValue(true);
        modelBuilder.Entity<Patient>().Property(x => x.IsActive).HasDefaultValue(true);
        modelBuilder.Entity<Room>().Property(x => x.IsActive).HasDefaultValue(true);
        modelBuilder.Entity<Patient>().Property(x => x.CreatedAt).HasDefaultValueSql("now()");

    }
}
