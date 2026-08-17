using AccountingSystem.Application.DTOs.Report;
using AccountingSystem.Application.Interfaces.Reports;
using AccountingSystem.Domain.Entities;
using AccountingSystem.Domain.Enums;
using AccountingSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace AccountingSystem.Infrastructure.Reports
{
    public class TransactionReportRepository : ITransactionReportRepository
    {

        private readonly AccountingDbContext _context;

        public TransactionReportRepository(AccountingDbContext context)
        {
            _context = context;
        }

        //public async Task<(IEnumerable<Transaction> Items, int TotalCount)> ReportAsync(
        //    int userId,
        //    TransactionReportFilterDto filter,
        //    int? pageNumber,
        //    int? pageSize)
        public async Task<(IEnumerable<Transaction> Items, int TotalCount)> ReportAsync(
    int userId,
    TransactionReportQueryDto filter,
    int? pageNumber,
    int? pageSize)
        {
            IQueryable<Transaction> query = _context.Transactions
                .AsNoTracking()
                .Include(x => x.Party)
                .Where(x =>
                    !x.IsDelete &&
                    x.Party.UserId == userId);

            if (filter.Type.HasValue)
                query = query.Where(x => x.Type == filter.Type.Value);

            if (filter.Status.HasValue)
                query = query.Where(x => x.Status == filter.Status.Value);

            if (filter.PartyId.HasValue)
                query = query.Where(x => x.PartyId == filter.PartyId.Value);

            if (filter.FromDate.HasValue)
            {
                var fromDate = filter.FromDate.Value.Date;

                query = query.Where(x =>
                    x.TransactionDate >= fromDate);
            }

            if (filter.ToDate.HasValue)
            {
                var toDate = filter.ToDate.Value.Date.AddDays(1);

                query = query.Where(x =>
                    x.TransactionDate < toDate);
            }

            var totalCount = await query.CountAsync();

            query = query.OrderByDescending(x => x.TransactionDate);

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            var items = await query.ToListAsync();

            return (items, totalCount);
        }

        public async Task<BalanceReportResponseDto> GetBalanceAsync(
            int userId,
            DateTime? fromDate,
            DateTime? toDate)
        {
            IQueryable<Transaction> query = _context.Transactions
                .Where(x => !x.IsDelete &&
                            x.Party.UserId == userId);

            if (fromDate.HasValue)
                query = query.Where(x => x.TransactionDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(x => x.TransactionDate <= toDate.Value);

            var result = await query
                .GroupBy(x => 1)
                .Select(g => new
                {
                    TotalReceived = g
                        .Where(x => x.Type == TransactionType.Received)
                        .Sum(x => (decimal?)x.Amount) ?? 0,

                    TotalPayment = g
                        .Where(x => x.Type == TransactionType.Payment)
                        .Sum(x => (decimal?)x.Amount) ?? 0
                })
                .FirstOrDefaultAsync();

            var totalReceived = result?.TotalReceived ?? 0;
            var totalPayment = result?.TotalPayment ?? 0;

            var balance = totalReceived - totalPayment;

            return new BalanceReportResponseDto
            {
                TotalReceived = totalReceived,
                TotalPayment = totalPayment,
                Balance = balance,
                Status = balance switch
                {
                    > 0 => BalanceStatus.Creditor,
                    < 0 => BalanceStatus.Debtor,
                    _ => BalanceStatus.Balanced
                }
            };
        }
    }
}
