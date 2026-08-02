using AccountingSystem.Application.Interfaces.Repositories;
using AccountingSystem.Domain.Entities;
using AccountingSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public async Task<IEnumerable<Transaction>> GetAllAsync(int userId)
        {
            return await _context.Transactions.Where(x=>x.Party.UserId==userId)
                .Include(x => x.Party)
                .ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id,int userId)
        {
            return await _context.Transactions.Where(x=>x.Party.UserId== userId)
                .Include(x => x.Party)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
        }
    }
}
