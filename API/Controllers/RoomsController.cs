using API.Extensions;
using Application.DTOs.Rooms;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(
    IRoomService service,
    ILogger<RoomsController> logger) : ControllerBase
{
    private readonly IRoomService _service = service;
    private readonly ILogger<RoomsController> _logger = logger;
    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var rooms = await _service.GetRoomsAsync();
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(rooms);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoom(int id)
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var room = await _service.GetRoomAsync(id);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(room);
    }

    [HttpPost]
    public async Task<IActionResult> AddRoom(RoomDTO roomDTO)
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var add = await _service.AddRoomAsync(roomDTO);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(add);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRoom(RoomDTO roomDTO, int id)
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var update = await _service.UpdateRoomAsync(roomDTO, id);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(update);
    }

    [HttpPatch("{id:int}/deactivate")]
    public async Task<IActionResult> PatchRoom(DeactivateRoomDTO roomDTO, int id)
    {
        _logger.LogInformation("Request received: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        var deactivate = await _service.PatchRoomAsync(roomDTO, id);
        _logger.LogInformation("Response sent: {Method} {Path}{Query}", Request.Method, Request.Path, Request.QueryString);
        return this.ToActionResult(deactivate);
    }
}
