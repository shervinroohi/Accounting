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

    //[HttpGet]
    //public async Task<IActionResult> GetAll()
    //{
    //    return Ok(await _partyService.GetAllAsync());
    //}
    [HttpGet]
    [HttpGet]
    public async Task<IActionResult> GetAll(
    int? pageNumber,
    int? pageSize)
    {
        return Ok(await _partyService.GetAllAsync(
            pageNumber,
            pageSize));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreatePartyDto dto)
    {
        await _partyService.CreateAsync(dto);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _partyService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

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
