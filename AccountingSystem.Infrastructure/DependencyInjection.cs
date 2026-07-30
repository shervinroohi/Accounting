using AccountingSystem.Application.DTOs.General;
using AccountingSystem.Application.Interfaces;
using AccountingSystem.Infrastructure.Authentication;
using AccountingSystem.Infrastructure.Persistence;
using AccountingSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingSystem.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AccountingDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.Configure<JwtSettings>(
                configuration.GetSection("JwtSettings"));

            // Register Repositories
            services.AddScoped<IPartyRepository, PartyRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
