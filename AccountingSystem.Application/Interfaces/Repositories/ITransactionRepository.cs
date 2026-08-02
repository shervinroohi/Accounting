using AccountingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(int id,int userId);

        Task<IEnumerable<Transaction>> GetAllAsync(int userId);

        Task AddAsync(Transaction transaction);

        void Update(Transaction transaction);

        void Delete(Transaction transaction);

    }
}
