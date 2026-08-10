using AccountingSystem.Application.DTOs.Transaction;
using AccountingSystem.Application.Interfaces.Repositories.TransactionRepository;
using AccountingSystem.Domain.Entities;
using AccountingSystem.Domain.Enums;
using AccountingSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace AccountingSystem.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AccountingDbContext _context;

        public TransactionRepository(AccountingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }


        public void Delete(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
        }


        public async Task<(IEnumerable<Transaction> Items, int TotalCount)> GetAllAsync(
            int userId,
            int? pageNumber,
            int? pageSize)
        {
            IQueryable<Transaction> query = _context.Transactions
                .Where(x => x.Party.UserId == userId && !x.IsDelete)
                .Include(x => x.Party);

            var totalCount = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            var items = await query.ToListAsync();

            return (items, totalCount);
        }

        public async Task<Transaction?> GetByIdAsync(int id,int userId)
        {
            return await _context.Transactions.Where(x=>x.Party.UserId== userId&& x.IsDelete == false)
                .Include(x => x.Party)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
        }




    }
}
