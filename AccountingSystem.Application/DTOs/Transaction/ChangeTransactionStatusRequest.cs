using AccountingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.DTOs.Transaction
{
    public class ChangeTransactionStatusRequest
    {
        public TransactionStatus Status { get; set; }
    }
}
