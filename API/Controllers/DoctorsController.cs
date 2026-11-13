using API.Extensions;
using Application.DTOs.Doctors;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController(
    IDoctorService service,
    ILogger<DoctorsController> logger) : ControllerBase
{
    private readonly IDoctorService _service = service;
    private readonly ILogger<DoctorsController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = await _service.GetDoctorsAsync();
        return this.ToActionResult(doctors);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDoctor(int id)
    {
        var doctor = await _service.GetDoctorAsync(id);
        return this.ToActionResult(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> AddDoctor(DoctorDTO doctorDTO)
    {
        var add = await _service.AddDoctorAsync(doctorDTO);
        return this.ToActionResult(add);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePatient(DoctorDTO doctorDTO, int id)
    {
        var update = await _service.UpdateDoctorAsync(doctorDTO, id);

        return this.ToActionResult(update);
    }

    [HttpPatch("{id:int}/deactivate")]
    public async Task<IActionResult> PatchPatient(DeactivateDoctorDTO doctorDTO, int id)
    {
        var patch = await _service.PatchDoctorAsync(doctorDTO, id);
        return this.ToActionResult(patch);
    }
}
