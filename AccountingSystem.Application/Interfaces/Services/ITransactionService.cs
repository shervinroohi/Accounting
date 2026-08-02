using AccountingSystem.Application.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        //Task<IEnumerable<TransactionResponseDto>> GetAllAsync();

        Task CreateAsync(CreateTransactionRequestDto request);
    }
}
