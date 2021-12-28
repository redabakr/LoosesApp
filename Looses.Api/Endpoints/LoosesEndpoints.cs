using Looses.Application.Commands;
using Looses.Application.Queries;
using MediatR;

namespace Looses.Api.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        
       // app.MapGet("/looses/{id}", async (IMediator mediator, int id) => await mediator.Send(new GetLooseDetails.Query(id)));
        app.MapGet("/wells", async (IMediator mediator) => await mediator.Send(new GetWells.Query()));
        app.MapGet("/looses", async (IMediator mediator, string? wellName) => await mediator.Send(new GetLooses.Query(wellName)));
        app.MapPost("/loss", async (IMediator mediator, CreateLooseRecord.Command request) => await mediator.Send(request));
        app.MapPut("/looses", async (IMediator mediator, CreateLooseRecords.Command request) => await mediator.Send(request));
        app.MapPut("/looses/calculate-days-offline", async (IMediator mediator) => await mediator.Send(new CalculateDaysOffline.Command()));
    }
}