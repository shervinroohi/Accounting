using AccountingSystem.Application.DTOs.Transaction;
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

        public TransactionService(
            ITransactionRepository transactionRepository,
            IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateTransactionRequestDto request)
        {
            var transaction = request.ToEntity();

            await _transactionRepository.AddAsync(transaction);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
