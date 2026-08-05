using AccountingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.DTOs.Transaction
{
    public class BalanceReportResponse
    {
        public decimal TotalReceived { get; set; }

        public decimal TotalPayment { get; set; }

        public decimal Balance { get; set; }

        public BalanceStatus Status { get; set; }
    }
}
