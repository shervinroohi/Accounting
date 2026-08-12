using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.DTOs.Report
{
    public class BalanceReportRequestDto
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
