using AccountingSystem.Application.DTOs.Report;
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
        //Task<(IEnumerable<Transaction> Items, int TotalCount)> ReportAsync(
        //    int userId,
        //    TransactionReportFilterDto filter,
        //    int? pageNumber,
        //    int? pageSize);
        Task<(IEnumerable<Transaction> Items, int TotalCount)> ReportAsync(
    int userId,
    TransactionReportQueryDto filter,
    int? pageNumber,
    int? pageSize);

        Task<BalanceReportResponseDto> GetBalanceAsync(
        int userId,
        DateTime? fromDate,
        DateTime? toDate);
    }
}
