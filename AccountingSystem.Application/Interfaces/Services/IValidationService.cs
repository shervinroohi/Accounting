using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces.Services
{
    public interface IValidationService
    {
        Task ValidateAsync<T>(T model);
    }
}

