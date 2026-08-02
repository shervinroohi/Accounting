using AccountingSystem.Application.DTOs.Party;
using AccountingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Mappings
{

    public static class PartyMapper
    {
        public static PartyResponseDto ToResponse(this Party party)
        {
            return new PartyResponseDto
            {
                Id = party.Id,
                Name = party.Name,
                PhoneNumber = party.PhoneNumber
            };
        }
        public static Party ToEntity(this CreatePartyDto dto)
        {
            return new Party
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber
            };
        }
    }
}
