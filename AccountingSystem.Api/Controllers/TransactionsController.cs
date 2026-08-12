using AccountingSystem.Application.DTOs.Transaction;
using AccountingSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionsController:ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateTransaction(CreateTransactionRequestDto request)
    {
        var transactionId = await _transactionService.CreateAsync(request);

        return StatusCode(StatusCodes.Status201Created, new
        {
            id = transactionId
        });
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        int? pageNumber,
        int? pageSize)
    {
        var result = await _transactionService.GetAllAsync(
            pageNumber,
            pageSize);

        return Ok(result);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(
    int id,
    [FromBody] ChangeTransactionStatusRequestDto request)
    {
        await _transactionService.ChangeStatusAsync(id, request);

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var transaction = await _transactionService.GetByIdAsync(id);

        return Ok(transaction);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _transactionService.DeleteAsync(id);

        return NoContent();
    }


}
