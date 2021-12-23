using System.Reflection;
using Customer.Application.Logging;
using Customer.Application.Validation;
using Customer.Domain.Factories;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Customer.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerFactory, CustomerFactory>();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandHandlerLoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandHandlerValidationBehavior<,>));
        // services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestValidationBehavior<,>));
        // services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(RequestValidationBehavior<,>));
        return services;
    }
}