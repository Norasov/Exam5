namespace Application.DTOs.Doctors;

public class DoctorDTO
{
    public string FullName { get; set; } = null!;
    public string Specialty { get; set; } = null!;
    public DateOnly HireDat { get; set; }
    public bool IsActive { get; set; } = true;
}
