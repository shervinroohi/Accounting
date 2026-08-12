using AccountingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.DTOs.Transaction
{
    public class TransactionResponseDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }

        public TransactionStatus Status { get; set; }

        public DateTime TransactionDate { get; set; }

        public string? Description { get; set; }

        public int PartyId { get; set; }

        public string PartyName { get; set; } = "";
    }
}
