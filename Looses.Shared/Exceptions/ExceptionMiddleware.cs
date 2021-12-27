using System.Text.Json;
using Looses.Shared.Abstraction.Exception;
using Microsoft.AspNetCore.Http;

namespace Looses.Shared.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (RequestValidationException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.Headers.Add("content-type", "application/json");
            var json = JsonSerializer.Serialize(new 
                {
                    ErrorCode = "validation_error",
                    ex.Message, 
                    Validations = ex.ErrorsDictionary
                }
            );
            await context.Response.WriteAsync(json);
        }
        catch (LoosesServiceBaseException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.Headers.Add("content-type", "application/json");

            var errorCode = ToUnderscoreCase(ex.GetType().Name.Replace("Exception", string.Empty));
            var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, ex.Message });
            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 400;
            context.Response.Headers.Add("content-type", "application/json");
            
            var json = JsonSerializer.Serialize(new { ErrorCode = 500, ex.Message });
            await context.Response.WriteAsync(json);
        }
    }

    private static string ToUnderscoreCase(string value)
        => string.Concat((value ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(value[i-1]) ? $"_{x}" : x.ToString())).ToLower();
}