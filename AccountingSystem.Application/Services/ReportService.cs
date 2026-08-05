using AccountingSystem.Application.DTOs.Transaction;
using AccountingSystem.Application.Interfaces.Auth;
using AccountingSystem.Application.Interfaces.Reports;
using AccountingSystem.Application.Interfaces.Repositories;
using AccountingSystem.Application.Interfaces.UOW;
using AccountingSystem.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Services
{
    public class ReportService:ITransactionReportService
    {

        private readonly ITransactionReportRepository _transactionReportRepository;
        private readonly ICurrentUserService _currentUserService;

        public ReportService(
            ITransactionReportRepository transactionReportRepository,
            ICurrentUserService currentUserService)
        {
            _transactionReportRepository = transactionReportRepository;
            _currentUserService = currentUserService;
        }
        public async Task<IEnumerable<TransactionResponseDto>> ReportAsync(
            TransactionReportFilterDto filter)
        {
            var userId = _currentUserService.UserId;

            var transactions = await _transactionReportRepository.ReportAsync(
                userId,
                filter);

            return transactions
                .Select(x => x.ToDto())
                .ToList();
        }

        public async Task<BalanceReportResponse> GetBalanceAsync(BalanceReportRequest request)
        {
            var userId = _currentUserService.UserId;

            return await _transactionReportRepository.GetBalanceAsync(
                userId,
                request.FromDate,
                request.ToDate);
        }
    }
}
