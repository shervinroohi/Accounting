using AccountingSystem.Application.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Mappings
{
    public static class BalanceReportMapper
    {
        public static BalanceReportQueryDto ToQuery(
            this BalanceReportRequestDto dto)
        {
            return new BalanceReportQueryDto
            {
                FromDate = ParseDate(dto.FromDate),
                ToDate = ParseDate(dto.ToDate)
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
                CultureInfo.InvariantCulture,
                DateTimeStyles.None);
        }
    }
}
