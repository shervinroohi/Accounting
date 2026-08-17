using AccountingSystem.Application.DTOs.General;
using AccountingSystem.Application.DTOs.Party;
using AccountingSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PartiesController:ControllerBase
{
    private readonly IPartyService _partyService;

    public PartiesController(IPartyService partyService)
    {
        _partyService = partyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] PaginationRequestDto request)
    {
        return Ok(await _partyService.GetAllAsync(
            request));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreatePartyDto dto)
    {
        var partyId = await _partyService.CreateAsync(dto);

        return StatusCode(StatusCodes.Status201Created, new
        {
            id = partyId
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _partyService.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePartyDto dto)
    {
        await _partyService.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _partyService.DeleteAsync(id);

        return NoContent();
    }
}
