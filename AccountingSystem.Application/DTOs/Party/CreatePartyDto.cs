using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.DTOs.Party
{
    public class CreatePartyDto
    {
        public string Name { get; set; } = "";

        public string PhoneNumber { get; set; } = "";
    }
}
