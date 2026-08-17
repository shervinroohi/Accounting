using AccountingSystem.Application.Exceptions;
using AccountingSystem.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingSystem.Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ValidateAsync<T>(T model)
        {
            var validator =
                _serviceProvider.GetService<FluentValidation.IValidator<T>>();

            if (validator is null)
                return;

            var result = await validator.ValidateAsync(model);

            if (result.IsValid)
                return;

            var errors = result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray());

            throw new ValidationException(errors);
        }
    }
}
