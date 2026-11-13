using API.Extensions;
using Application.DTOs.Patients;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(
    IPatientService service,
    ILogger<PatientsController> logger) : ControllerBase
{
    private readonly IPatientService _service = service;
    private readonly ILogger<PatientsController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        var patients = await _service.GetPatientsAsync();
        return this.ToActionResult(patients);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var patient = await _service.GetPatientAsync(id);
        return this.ToActionResult(patient);
    }

    [HttpPost]
    public async Task<IActionResult> AddPatient(PatientDTO patientDTO)
    {
        var add = await _service.AddPatientAsync(patientDTO);
        return this.ToActionResult(add);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePatient(PatientDTO patientDTO, int id)
    {
        var update = await _service.UpdatePatientAsync(patientDTO, id);
        return this.ToActionResult(update);
    }

    [HttpPatch("{id:int}/deactivate")]
    public async Task<IActionResult> PatchPatient(DeactivatePatientDTO patientDTO, int id)
    {
        var deactivate = await _service.PatchPatientAsync(patientDTO, id);
        return this.ToActionResult(deactivate);
    }
}
