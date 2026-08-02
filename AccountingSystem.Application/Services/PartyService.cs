using AccountingSystem.Application.DTOs.Party;
using AccountingSystem.Application.Interfaces.Auth;
using AccountingSystem.Application.Interfaces.Repositories;
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

        public PartyService(
        IPartyRepository partyRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
        {
            _partyRepository = partyRepository;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PartyResponseDto>> GetAllAsync()
        {
            var parties = await _partyRepository
                .GetAllAsync(_currentUser.UserId);

            return parties.Select(x => x.ToResponse());
        }

        public async Task CreateAsync(CreatePartyDto dto)
        {
            var party = dto.ToEntity();
            party.UserId = _currentUser.UserId;

            await _partyRepository.AddAsync(party);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PartyResponseDto?> GetByIdAsync(int id)
        {
            var party = await _partyRepository
                .GetByIdAsync(id, _currentUser.UserId);

            if (party == null)
                return null;

            return party.ToResponse();
        }

        public async Task UpdateAsync(int id, UpdatePartyDto dto)
        {
            var party = await _partyRepository
                .GetByIdAsync(id, _currentUser.UserId);

            if (party == null)
                throw new Exception("Party not found.");

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
                throw new Exception("Party not found.");

            _partyRepository.Delete(party);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
