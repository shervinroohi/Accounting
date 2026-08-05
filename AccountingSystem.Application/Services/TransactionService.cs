using AccountingSystem.Application.DTOs.Transaction;
using AccountingSystem.Application.Interfaces.Auth;
using AccountingSystem.Application.Interfaces.Repositories;
using AccountingSystem.Application.Interfaces.Services;
using AccountingSystem.Application.Interfaces.UOW;
using AccountingSystem.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task CreateAsync(CreateTransactionRequestDto request)
        {
            var transaction = request.ToEntity();

            await _transactionRepository.AddAsync(transaction);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync()
        {
            var userId = _currentUserService.UserId;

            var transactions = await _transactionRepository.GetAllAsync(userId);

            return transactions.Select(x => x.ToDto());
        }

        public async Task ChangeStatusAsync(int id, ChangeTransactionStatusRequest request)
        {
            var userId = _currentUserService.UserId;

            var transaction = await _transactionRepository.GetByIdAsync(id, userId);

            if (transaction is null)
                throw new Exception("Transaction not found.");

            transaction.Status = request.Status;

            _transactionRepository.Update(transaction);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userId = _currentUserService.UserId;

            var transaction = await _transactionRepository.GetByIdAsync(id, userId);

            if (transaction is null)
                throw new Exception("Transaction not found.");

            transaction.IsDelete = true;

            _transactionRepository.Update(transaction);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TransactionResponseDto> GetByIdAsync(int id)
        {
            var userId = _currentUserService.UserId;

            var transaction = await _transactionRepository.GetByIdAsync(id, userId);

            if (transaction is null)
                throw new Exception("Transaction not found.");

            return transaction.ToDto();
        }


    }
}
