namespace Application.DTOs.Patients;

public class PatientDTO
{
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateOnly? BirthDate { get; set; }
    public bool IsActive { get; set; } = true;

}
