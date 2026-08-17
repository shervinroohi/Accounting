using AccountingSystem.Application.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Mappings
{
    public static class TransactionReportMapper
    {
        public static TransactionReportQueryDto ToQuery(
            this TransactionReportFilterDto filter)
        {
            return new TransactionReportQueryDto
            {
                Type = filter.Type,
                Status = filter.Status,
                PartyId = filter.PartyId,
                FromDate = ParseDate(filter.FromDate),
                ToDate = ParseDate(filter.ToDate)
            };
        }

        private static DateTime? ParseDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            return DateTime.ParseExact(
                value,
                new[]
                {
                    "yyyy-MM-dd",
                    "yyyy-MM-ddTHH:mm:ss"
                },
                CultureInfo.InvariantCulture);
        }
    }
}
