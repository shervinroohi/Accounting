using AccountingSystem.Application.Interfaces.Auth;
using AccountingSystem.Application.Interfaces.Services;
using AccountingSystem.Application.Services;
using AccountingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPartyService, PartyService>();
            services.AddScoped<ITransactionService, TransactionService>();

            

            return services;
        }
    }
}
