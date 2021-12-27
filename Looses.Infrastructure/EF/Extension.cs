using Looses.Application.Services;
using Looses.Domain.Repositories;
using Looses.Infrastructure.EF.Options;
using Looses.Infrastructure.EF.Repositories;
using Looses.Infrastructure.EF.Services;
using Looses.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Looses.Infrastructure.EF;

public static class Extension
{
    public static IServiceCollection AddSql(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<SQLOptions>("SQLOptions");
        services.AddDbContext<LoosesDbContext>(ctx => ctx.UseSqlServer(options.ConnectionString));

        services.AddScoped<ILooseRepository, EfLooseRepository>();
        services.AddScoped<ILoosesReadService, EfLoosesReadService>();
        
        return services;
    }
}