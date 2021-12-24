using System.Reflection;
using Customer.Application.Logging;
using Customer.Application.Validation;
using Customer.Domain.Factories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Customer.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddSingleton<ICustomerFactory, CustomerFactory>();
        services.AddMediatR(assembly);
        services.AddValidatorsFromAssemblyContaining(typeof(IAssemblyMarker));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandHandlerLoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandHandlerValidationBehavior<,>));

        return services;
    }
}