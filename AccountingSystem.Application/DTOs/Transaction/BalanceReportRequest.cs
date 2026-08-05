using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.DTOs.Transaction
{
    public class BalanceReportRequest
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
