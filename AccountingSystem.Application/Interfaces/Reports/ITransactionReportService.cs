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
        Task<IEnumerable<TransactionResponseDto>> ReportAsync(TransactionReportFilterDto filter);

        Task<BalanceReportResponse> GetBalanceAsync(BalanceReportRequest request);
    }
}
