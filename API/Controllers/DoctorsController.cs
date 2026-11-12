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
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var doctors = await _service.GetDoctorsAsync();
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
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
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var add = await _service.AddDoctorAsync(doctorDTO);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(add);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePatient(DoctorDTO doctorDTO, int id)
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var update = await _service.UpdateDoctorAsync(doctorDTO, id);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);

        return this.ToActionResult(update);
    }

    [HttpPatch("{id:int}/deactivate")]
    public async Task<IActionResult> PatchPatient(DeactivateDoctorDTO doctorDTO, int id)
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var patch = await _service.PatchDoctorAsync(doctorDTO, id);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(patch);
    }
}
