using AccountingSystem.Application.DTOs.Report;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Validators.Report
{
    public class BalanceReportValidator
        : AbstractValidator<BalanceReportRequestDto>
    {
        private static readonly string[] AllowedDateFormats =
        {
            "yyyy-MM-dd",
            "yyyy-MM-ddTHH:mm:ss"
        };

        public BalanceReportValidator()
        {

            RuleFor(x => x.FromDate)
                .Must(BeValidDate)
                .When(x => !string.IsNullOrWhiteSpace(x.FromDate))
                .WithMessage(
                    "FromDate must be in the format yyyy-MM-dd or yyyy-MM-ddTHH:mm:ss.");


            RuleFor(x => x.ToDate)
                .Must(BeValidDate)
                .When(x => !string.IsNullOrWhiteSpace(x.ToDate))
                .WithMessage(
                    "ToDate must be in the format yyyy-MM-dd or yyyy-MM-ddTHH:mm:ss.");


            RuleFor(x => x)
                .Must(HaveValidDateRange)
                .WithMessage(
                    "FromDate must be earlier than or equal to ToDate.");
        }


        private static bool BeValidDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return true;

            return DateTime.TryParseExact(
                value,
                AllowedDateFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _);
        }


        private static bool HaveValidDateRange(
            BalanceReportRequestDto request)
        {

            if (string.IsNullOrWhiteSpace(request.FromDate) ||
                string.IsNullOrWhiteSpace(request.ToDate))
                return true;


            var fromValid = DateTime.TryParseExact(
                request.FromDate,
                AllowedDateFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var fromDate);

            var toValid = DateTime.TryParseExact(
                request.ToDate,
                AllowedDateFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var toDate);


            if (!fromValid || !toValid)
                return true;


            return fromDate <= toDate;
        }
    }
}
