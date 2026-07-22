using AccountingSystem.Application.Interfaces;
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
    public class PartyRepository: IPartyRepository
    {
        private readonly AccountingDbContext _context;

        public PartyRepository(AccountingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Party party)
        {
            await _context.Parties.AddAsync(party);
        }

        public void Delete(Party party)
        {
            party.IsDelete = true;
            _context.Parties.Update(party);
        }

        public async Task<IEnumerable<Party>> GetAllAsync()
        {
            return await _context.Parties
                .Where(x => !x.IsDelete)
                .Include(x => x.Transactions)
                .ToListAsync();
        }

        public async Task<Party?> GetByIdAsync(int id)
        {
            return await _context.Parties
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Party party)
        {
            _context.Parties.Update(party);
        }
    }
}
