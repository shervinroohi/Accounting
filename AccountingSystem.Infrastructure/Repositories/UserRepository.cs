using AccountingSystem.Application.Interfaces.Repositories.UserRepository;
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
    public class UserRepository:IUserRepository
    {
        private readonly AccountingDbContext _context;

        public UserRepository(AccountingDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _context.Users
                .AnyAsync(u => u.UserName == userName);
        }
        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
