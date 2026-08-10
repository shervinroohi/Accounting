using AccountingSystem.Application.DTOs.General;
using AccountingSystem.Application.Interfaces.Auth;
using AccountingSystem.Application.Interfaces.Reports;
using AccountingSystem.Application.Interfaces.Repositories.PatyRespository;
using AccountingSystem.Application.Interfaces.Repositories.TransactionRepository;
using AccountingSystem.Application.Interfaces.Repositories.UserRepository;
using AccountingSystem.Application.Interfaces.UOW;
using AccountingSystem.Infrastructure.Authentication;
using AccountingSystem.Infrastructure.Persistence;
using AccountingSystem.Infrastructure.Reports;
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

            services.AddHttpContextAccessor();
            services.AddScoped<IPartyRepository, PartyRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITransactionReportRepository, TransactionReportRepository>();

            return services;
        }
    }
}
