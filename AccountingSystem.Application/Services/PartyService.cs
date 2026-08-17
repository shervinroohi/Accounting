using AccountingSystem.Application.DTOs.General;
using AccountingSystem.Application.DTOs.Party;
using AccountingSystem.Application.Exceptions;
using AccountingSystem.Application.Interfaces.Auth;
using AccountingSystem.Application.Interfaces.Repositories.PatyRespository;
using AccountingSystem.Application.Interfaces.Services;
using AccountingSystem.Application.Interfaces.UOW;
using AccountingSystem.Application.Mappings;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Application.Services
{
    public class PartyService : IPartyService
    {
        private readonly IPartyRepository _partyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationService _validationService;

        public PartyService(
        IPartyRepository partyRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork,
        IValidationService validationService)
        {
            _partyRepository = partyRepository;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _validationService = validationService;
        }

        public async Task<PagedResultDto<PartyResponseDto>> GetAllAsync(
            PaginationRequestDto pagination)
        {
            await _validationService.ValidateAsync(pagination);

            var result = await _partyRepository.GetAllAsync(
                _currentUser.UserId,
                pagination.PageNumber,
                pagination.PageSize);

            var items = result.Items
                .Select(x => x.ToResponse())
                .ToList();

            return new PagedResultDto<PartyResponseDto>
            {
                Items = items,
                PageNumber = pagination.PageNumber ?? 0,
                PageSize = pagination.PageSize ?? 0,
                TotalCount = result.TotalCount,
                TotalPages = pagination.PageNumber.HasValue && pagination.PageSize.HasValue
                    ? (int)Math.Ceiling(
                        result.TotalCount / (double)pagination.PageSize.Value)
                    : 1
            };
        }

        public async Task<int> CreateAsync(CreatePartyDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var party = dto.ToEntity();
            party.UserId = _currentUser.UserId;

            await _partyRepository.AddAsync(party);

            await _unitOfWork.SaveChangesAsync();

            return party.Id;
        }

        public async Task<PartyResponseDto?> GetByIdAsync(int id)
        {
            var party = await _partyRepository
                .GetByIdAsync(id, _currentUser.UserId);

            if (party == null)
                throw new NotFoundException("No Party with this ID was found.");

            return party.ToResponse();
        }

        public async Task UpdateAsync(int id, UpdatePartyDto dto)
        {
            await _validationService.ValidateAsync(dto);

            var party = await _partyRepository
                .GetByIdAsync(id, _currentUser.UserId);

            if (party == null)
                throw new NotFoundException("No Party with this ID was found.");

            party.Name = dto.Name;
            party.PhoneNumber = dto.PhoneNumber;

            _partyRepository.Update(party);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var party = await _partyRepository
                .GetByIdAsync(id, _currentUser.UserId);

            if (party == null)
                throw new NotFoundException("No Party with this ID was found.");

            _partyRepository.Delete(party);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
