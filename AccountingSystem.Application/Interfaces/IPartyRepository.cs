using AccountingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces
{
    public interface IPartyRepository
    {
        Task<Party?> GetByIdAsync(int id);

        Task<IEnumerable<Party>> GetAllAsync();

        Task AddAsync(Party party);

        void Update(Party party);

        void Delete(Party party);
    }
}
