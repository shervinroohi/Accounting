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

        Task<PagedResultDto<TransactionResponseDto>> GetAllAsync(
            PaginationRequestDto dto);

        Task<int> CreateAsync(CreateTransactionRequestDto request);

        Task ChangeStatusAsync(int id, ChangeTransactionStatusRequestDto request);

        Task<TransactionResponseDto> GetByIdAsync(int id);

        Task DeleteAsync(int id);


    }
}
