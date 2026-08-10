using AccountingSystem.Application.DTOs.General;
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
        public async Task<PagedResultDto<TransactionResponseDto>> ReportAsync(
            TransactionReportFilterDto filter,
            int? pageNumber,
            int? pageSize)
        {
            var userId = _currentUserService.UserId;

            var result = await _transactionReportRepository.ReportAsync(
                userId,
                filter,
                pageNumber,
                pageSize);

            var items = result.Items
                .Select(x => x.ToDto())
                .ToList();

            return new PagedResultDto<TransactionResponseDto>
            {
                Items = items,
                PageNumber = pageNumber ?? 0,
                PageSize = pageSize ?? 0,
                TotalCount = result.TotalCount,
                TotalPages = pageNumber.HasValue && pageSize.HasValue
                    ? (int)Math.Ceiling(
                        result.TotalCount / (double)pageSize.Value)
                    : 1
            };
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
