using AccountingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.DTOs.Report
{
    public class TransactionReportFilterDto
    {
        public TransactionType? Type { get; set; }

        public TransactionStatus? Status { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int? PartyId { get; set; }
    }
}
