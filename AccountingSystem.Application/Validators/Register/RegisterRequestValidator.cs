using AccountingSystem.Application.DTOs.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Validators.Register
{
    public class RegisterRequestValidator
        : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required.")
                .Matches(@"^[^\u0600-\u06FF]+$")
                .WithMessage("Username must not contain Persian characters.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters.")
                .Matches(@"^[^\u0600-\u06FF]+$")
                .WithMessage("Password must not contain Persian characters.");
        }
    }
}
