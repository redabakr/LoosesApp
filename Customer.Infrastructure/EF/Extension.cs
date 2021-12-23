using Customer.Application.Services;
using Customer.Domain.Repositories;
using Customer.Infrastructure.EF.Options;
using Customer.Infrastructure.EF.Repositories;
using Customer.Infrastructure.EF.Services;
using Customer.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Infrastructure.EF;

public static class Extension
{
    public static IServiceCollection AddSql(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<SQLOptions>("SQLOptions");
        services.AddDbContext<CustomerDbContext>(ctx => ctx.UseSqlServer(options.ConnectionString));

        services.AddScoped<ICustomerRepository, EfCustomerRepository>();
        services.AddScoped<ICustomerReadService, EfCustomerReadService>();
        return services;
    }
}