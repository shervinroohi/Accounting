using AccountingSystem.Application.DTOs.Transaction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Validators.Transaction
{
    public class CreateTransactionValidator
        : AbstractValidator<CreateTransactionRequestDto>
    {
        private static readonly string[] AllowedDateFormats =
        {
            "yyyy-MM-dd",
            "yyyy-MM-ddTHH:mm:ss"
        };

        public CreateTransactionValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.PartyId)
                .GreaterThan(0)
                .WithMessage("PartyId must be greater than 0.");

            RuleFor(x => x.TransactionDate)
                .NotEmpty()
                .WithMessage("TransactionDate is required.")
                .Must(BeValidDate)
                .When(x => !string.IsNullOrWhiteSpace(x.TransactionDate))
                .WithMessage(
                    "TransactionDate must be in the format yyyy-MM-dd or yyyy-MM-ddTHH:mm:ss.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage(
                    "Description cannot be longer than 500 characters.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage(
                    "Type must be either Payment or Received.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage(
                    "Status must be either Settled or UnSettled.");
        }

        private static bool BeValidDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return DateTime.TryParseExact(
                value,
                AllowedDateFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _);
        }
    }
}
