using AccountingSystem.Application.DTOs.General;
using AccountingSystem.Application.DTOs.Report;
using AccountingSystem.Application.DTOs.Transaction;
using AccountingSystem.Application.Interfaces.Auth;
using AccountingSystem.Application.Interfaces.Reports;
using AccountingSystem.Application.Interfaces.Repositories;
using AccountingSystem.Application.Interfaces.Services;
using AccountingSystem.Application.Interfaces.UOW;
using AccountingSystem.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Services
{


    public class ReportService:ITransactionReportService
    {

        private readonly ITransactionReportRepository _transactionReportRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IValidationService _validatorService;

        public ReportService(
            ITransactionReportRepository transactionReportRepository,
            ICurrentUserService currentUserService,
            IValidationService validatorService)
        {
            _transactionReportRepository = transactionReportRepository;
            _currentUserService = currentUserService;
            _validatorService = validatorService;
        }
        //public async Task<PagedResultDto<TransactionResponseDto>> ReportAsync(
        //    TransactionReportFilterDto filter,
        //    PaginationRequestDto pagination)
        //{
        //    await _validatorService.ValidateAsync(pagination);

        //    var userId = _currentUserService.UserId;

        //    var result = await _transactionReportRepository.ReportAsync(
        //        userId,
        //        filter,
        //        pagination.PageNumber,
        //        pagination.PageSize);

        //    var items = result.Items
        //        .Select(x => x.ToDto())
        //        .ToList();

        //    return new PagedResultDto<TransactionResponseDto>
        //    {
        //        Items = items,
        //        PageNumber = pagination.PageNumber ?? 0,
        //        PageSize = pagination.PageSize ?? 0,
        //        TotalCount = result.TotalCount,
        //        TotalPages = pagination.PageNumber.HasValue && pagination.PageSize.HasValue
        //            ? (int)Math.Ceiling(
        //                result.TotalCount / (double)pagination.PageSize.Value)
        //            : 1
        //    };
        //}
        public async Task<PagedResultDto<TransactionResponseDto>> ReportAsync(
    TransactionReportFilterDto filter,
    PaginationRequestDto pagination)
        {
            await _validatorService.ValidateAsync(filter);
            await _validatorService.ValidateAsync(pagination);

            var userId = _currentUserService.UserId;

            var queryFilter = filter.ToQuery();

            var result = await _transactionReportRepository.ReportAsync(
                userId,
                queryFilter,
                pagination.PageNumber,
                pagination.PageSize);

            var items = result.Items
                .Select(x => x.ToDto())
                .ToList();

            return new PagedResultDto<TransactionResponseDto>
            {
                Items = items,
                PageNumber = pagination.PageNumber ?? 0,
                PageSize = pagination.PageSize ?? 0,
                TotalCount = result.TotalCount,
                TotalPages = pagination.PageNumber.HasValue &&
                             pagination.PageSize.HasValue
                    ? (int)Math.Ceiling(
                        result.TotalCount /
                        (double)pagination.PageSize.Value)
                    : 1
            };
        }

        public async Task<BalanceReportResponseDto> GetBalanceAsync(BalanceReportRequestDto request)
        {
            var userId = _currentUserService.UserId;

            return await _transactionReportRepository.GetBalanceAsync(
                userId,
                request.FromDate,
                request.ToDate);
        }
    }
}
