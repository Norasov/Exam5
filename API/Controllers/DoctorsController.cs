using Application.Interfaces;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController(IDoctorService service) : ControllerBase
{
        private readonly IDoctorService _service = service;
        
    [HttpGet]
    public async Task<ApiResponse<List<Doctor>>> GetDoctors()
    {
        return await _service.GetDoctorsAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Doctor?>> GetDoctor(int id)
    {
        return await _service.GetDoctorAsync(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> AddDoctor(Doctor doctor)
    {
        return await _service.AddDoctorAsync(doctor);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> UpdatePatient(Doctor doctor)
    {
        return await _service.UpdateDoctorAsync(doctor);
    }

    [HttpPatch]
    public async Task<ApiResponse<bool>> PatchPatient(Doctor doctor)
    {
        return await _service.PatchDoctorAsync(doctor);
    }

}
