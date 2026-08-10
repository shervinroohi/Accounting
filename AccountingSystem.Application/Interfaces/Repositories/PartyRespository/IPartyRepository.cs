using AccountingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces.Repositories.PatyRespository
{
    public interface IPartyRepository
    {
        Task<Party?> GetByIdAsync(int id,int userId);

        //Task<IEnumerable<Party>> GetAllAsync(int userId);
        Task<(IEnumerable<Party> Items, int TotalCount)> GetAllAsync(
            int userId,
            int? pageNumber,
            int? pageSize);

        Task AddAsync(Party party);

        void Update(Party party);

        void Delete(Party party);
    }
}
