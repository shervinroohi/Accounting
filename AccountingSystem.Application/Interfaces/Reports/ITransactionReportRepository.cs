using AccountingSystem.Application.DTOs.Transaction;
using AccountingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces.Reports
{
    public interface ITransactionReportRepository
    {
        Task<(IEnumerable<Transaction> Items, int TotalCount)> ReportAsync(
            int userId,
            TransactionReportFilterDto filter,
            int? pageNumber,
            int? pageSize);

        Task<BalanceReportResponse> GetBalanceAsync(
        int userId,
        DateTime? fromDate,
        DateTime? toDate);
    }
}
