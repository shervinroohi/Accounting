using AccountingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.DTOs.Transaction
{
    public class CreateTransactionRequestDto
    {
        public decimal Amount { get; set; }


        public TransactionType Type { get; set; } 

        public TransactionStatus Status { get; set; }


        public string? TransactionDate { get; set; }

        public string? Description { get; set; }

        public int PartyId { get; set; }
    }
}
