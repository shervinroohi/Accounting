using AccountingSystem.Application.Interfaces.Repositories.PatyRespository;
using AccountingSystem.Domain.Entities;
using AccountingSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


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

        public async Task<(IEnumerable<Party> Items, int TotalCount)> GetAllAsync(
            int userId,
            int? pageNumber,
            int? pageSize)
        {
            var query = _context.Parties
                .Where(x => !x.IsDelete && x.UserId == userId);

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

        public async Task<Party?> GetByIdAsync(int id,int userId)
        {
            return await _context.Parties
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId==userId &&!x.IsDelete);
        }

        public async Task<Party?> GetByIdForUserAsync(
        int partyId,
        int userId)
        {
            return await _context.Parties
                .FirstOrDefaultAsync(x =>
                    x.Id == partyId &&
                    x.UserId == userId &&
                    !x.IsDelete);
        }
        public void Update(Party party)
        {
            _context.Parties.Update(party);
        }
    }
}
