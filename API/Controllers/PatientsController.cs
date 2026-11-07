using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IPatientService service) : ControllerBase
{
    private readonly IPatientService _service = service;

    [HttpGet]
    public async Task<ApiResponse<List<Patient>>> GetPatients()
    {
        return await _service.GetPatientsAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Patient?>> GetPatient(int id)
    {
        return await _service.GetPatientAsync(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> AddPatient(Patient patient)
    {
        return await _service.AddPatientAsync(patient);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> UpdatePatient(Patient patient)
    {
        return await _service.UpdatePatientAsync(patient);
    }

    [HttpPatch]
    public async Task<ApiResponse<bool>> PatchPatient(Patient patient)
    {
        return await _service.PatchPatientAsync(patient);
    }
}
