using AccountingSystem.Application.DTOs.General;
using AccountingSystem.Application.DTOs.Report;
using AccountingSystem.Application.Interfaces.Reports;
using AccountingSystem.Application.Interfaces.Services;
using AccountingSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController:ControllerBase
    {
        private readonly ITransactionReportService _transactionReportService;

        public ReportController(ITransactionReportService transactionReportService)
        {
            _transactionReportService = transactionReportService;
        }
        [HttpGet("report")]
        public async Task<IActionResult> Report(
            [FromQuery] TransactionReportFilterDto filter,
            [FromQuery] PaginationRequestDto pagination)
        {
            var result = await _transactionReportService.ReportAsync(
                filter,
                pagination);

            return Ok(result);
        }
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance([FromQuery] BalanceReportRequestDto request)
        {
            var result = await _transactionReportService.GetBalanceAsync(request);

            return Ok(result);
        }
    }
}
