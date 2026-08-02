using AccountingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByIdAsync(int id);

        Task<bool> UserNameExistsAsync(string userName);
    }
}
