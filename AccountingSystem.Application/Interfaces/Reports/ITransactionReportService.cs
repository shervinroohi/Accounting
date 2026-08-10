using AccountingSystem.Application.DTOs.General;
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
            int? pageNumber,
            int? pageSize);

        Task<BalanceReportResponse> GetBalanceAsync(BalanceReportRequest request);
    }
}
