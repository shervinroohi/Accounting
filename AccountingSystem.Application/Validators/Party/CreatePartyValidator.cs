using AccountingSystem.Application.DTOs.Party;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Validators.Party
{
    public class CreatePartyValidator
        : AbstractValidator<CreatePartyDto>
    {
        public CreatePartyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Matches(@"^[a-zA-Z0-9\s]+$")
                .WithMessage("Name must contain only English letters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^\+[1-9]\d{7,14}$")
                .WithMessage(
                    "Phone number must be in international format. Example: +989123456789.");
        }
    }
}