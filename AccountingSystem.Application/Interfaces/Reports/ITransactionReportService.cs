using AccountingSystem.Application.DTOs.General;
using AccountingSystem.Application.DTOs.Report;
using AccountingSystem.Application.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces.Reports
{
    public interface ITransactionReportService
    {
        Task<PagedResultDto<TransactionResponseDto>> ReportAsync(
            TransactionReportFilterDto filter,
            PaginationRequestDto pagination);

        Task<BalanceReportResponseDto> GetBalanceAsync(BalanceReportRequestDto request);
    }
}
