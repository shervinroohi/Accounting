using AccountingSystem.Application.DTOs.Transaction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Validators.Transaction
{
    public class CreateTransactionValidator
        : AbstractValidator<CreateTransactionRequestDto>
    {
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
                .WithMessage("TransactionDate is required.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot be longer than 500 characters.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Invalid transaction type.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid transaction status.");
        }
    }
}
