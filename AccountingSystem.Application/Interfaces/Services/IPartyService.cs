using AccountingSystem.Application.DTOs.Party;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces.Services
{
    public interface IPartyService
    {
        Task<IEnumerable<PartyResponseDto>> GetAllAsync();

        Task CreateAsync(CreatePartyDto dto);

        Task<PartyResponseDto?> GetByIdAsync(int id);

        Task UpdateAsync(int id, UpdatePartyDto dto);

        Task DeleteAsync(int id);
    }
}
