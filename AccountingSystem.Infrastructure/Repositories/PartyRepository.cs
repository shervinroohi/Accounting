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

        public async Task<IEnumerable<Party>> GetAllAsync(int userId)
        {
            return await _context.Parties
                .Where(x => !x.IsDelete&&x.UserId==userId)
                .ToListAsync();
        }

        public async Task<Party?> GetByIdAsync(int id,int userId)
        {
            return await _context.Parties
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId==userId &&!x.IsDelete);
        }


        public void Update(Party party)
        {
            _context.Parties.Update(party);
        }
    }
}
