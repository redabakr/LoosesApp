using System.Reflection;
using FluentValidation;
using Looses.Application.Logging;
using Looses.Application.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Looses.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(assembly);
        services.AddValidatorsFromAssemblyContaining(typeof(IAssemblyMarker));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandHandlerLoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandHandlerValidationBehavior<,>));

        return services;
    }
}