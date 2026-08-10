using AccountingSystem.Application.DTOs.General;
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
        Task<PagedResultDto<TransactionResponseDto>> GetAllAsync(
            int? pageNumber,
            int? pageSize);

        Task CreateAsync(CreateTransactionRequestDto request);

        Task ChangeStatusAsync(int id, ChangeTransactionStatusRequest request);

        Task<TransactionResponseDto> GetByIdAsync(int id);

        Task DeleteAsync(int id);


    }
}
