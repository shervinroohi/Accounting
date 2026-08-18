using AccountingSystem.Application.DTOs.General;
using AccountingSystem.Application.DTOs.Transaction;
using AccountingSystem.Application.Exceptions;
using AccountingSystem.Application.Interfaces.Auth;
using AccountingSystem.Application.Interfaces.Repositories.PatyRespository;
using AccountingSystem.Application.Interfaces.Repositories.TransactionRepository;
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
        private readonly IPartyRepository _partyRepository;
        private readonly IValidationService _validationService;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            IPartyRepository partyRepository,
            IValidationService validationService)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _partyRepository = partyRepository;
            _validationService = validationService;
        }


        public async Task<int> CreateAsync(CreateTransactionRequestDto request)
        {
            await _validationService.ValidateAsync(request);

            var userId = _currentUserService.UserId;

            var party = await _partyRepository.GetByIdForUserAsync(
                request.PartyId,
                userId);

            if (party is null)
                throw new NotFoundException("Party not found.");

            var transaction = request.ToEntity();

            await _transactionRepository.AddAsync(transaction);

            await _unitOfWork.SaveChangesAsync();

            return transaction.Id;
        }


        public async Task<PagedResultDto<TransactionResponseDto>> GetAllAsync(
            PaginationRequestDto dto)
        {
            await _validationService.ValidateAsync(dto);
            var userId = _currentUserService.UserId;

            var result = await _transactionRepository.GetAllAsync(
                userId,
                dto.PageNumber,
                dto.PageSize);

            var items = result.Items
                .Select(x => x.ToDto())
                .ToList();

            return new PagedResultDto<TransactionResponseDto>
            {
                Items = items,
                PageNumber = dto.PageNumber ?? 0,
                PageSize = dto.PageSize ?? 0,
                TotalCount = result.TotalCount,
                TotalPages = dto.PageNumber.HasValue && dto.PageSize.HasValue
                    ? (int)Math.Ceiling(
                        result.TotalCount / (double)dto.PageSize.Value)
                    : 1
            };
        }
       
        public async Task ChangeStatusAsync(int id, ChangeTransactionStatusRequestDto request)
        {
            await _validationService.ValidateAsync(request);
            var userId = _currentUserService.UserId;

            var transaction = await _transactionRepository.GetByIdAsync(id, userId);

            if (transaction is null)
                throw new NotFoundException("Transaction not found.");

            transaction.Status = request.Status;

            _transactionRepository.Update(transaction);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userId = _currentUserService.UserId;

            var transaction = await _transactionRepository.GetByIdAsync(id, userId);

            if (transaction is null)
                throw new NotFoundException("Transaction not found.");

            transaction.IsDelete = true;

            _transactionRepository.Update(transaction);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TransactionResponseDto> GetByIdAsync(int id)
        {
            var userId = _currentUserService.UserId;

            var transaction = await _transactionRepository.GetByIdAsync(id, userId);

            if (transaction is null)
                throw new NotFoundException("Transaction not found.");

            return transaction.ToDto();
        }


    }
}
