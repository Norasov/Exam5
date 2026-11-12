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
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var patients = await _service.GetPatientsAsync();
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(patients);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var patient = await _service.GetPatientAsync(id);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(patient);
    }

    [HttpPost]
    public async Task<IActionResult> AddPatient(PatientDTO patientDTO)
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var add = await _service.AddPatientAsync(patientDTO);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(add);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePatient(PatientDTO patientDTO, int id)
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var update = await _service.UpdatePatientAsync(patientDTO, id);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(update);
    }

    [HttpPatch("{id:int}/deactivate")]
    public async Task<IActionResult> PatchPatient(DeactivatePatientDTO patientDTO, int id)
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var deactivate = await _service.PatchPatientAsync(patientDTO, id);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(deactivate);
    }
}
