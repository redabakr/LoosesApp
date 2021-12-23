using MediatR;
using Microsoft.Extensions.Logging;

namespace Customer.Application.Logging;

public class CommandHandlerLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public CommandHandlerLoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var commandType = request.GetType().Name;

        try
        {
            _logger.LogInformation($"Started processing {commandType} command.");
              var response = await next();
            _logger.LogInformation($"Finished processing {commandType} command.");
            return response;
        }
        catch
        {
            _logger.LogError($"Failed to process {commandType} command.");
            throw; 
        }
    }
}
