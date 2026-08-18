using AccountingSystem.Application.DTOs.Transaction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Validators.Transaction
{
    public class ChangeTransactionStatusValidator
        : AbstractValidator<ChangeTransactionStatusRequestDto>
    {
        public ChangeTransactionStatusValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage(
                    "Status must be either Settled or UnSettled.");
        }
    }
}
