using Customer.Application.Commands;
using Customer.Application.Queries;
using MediatR;

namespace Customer.Api.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
       // app.MapGet("customers", async (IMediator mediator) => await mediator.Send(new GetAllCustomersRequest()));
        app.MapGet("/customers/{id}", async (IMediator mediator, Guid id) => await mediator.Send(new GetCustomer.Query(id)));
        app.MapPost("/customers", async (IMediator mediator, CreateCustomer.Command request) => await mediator.Send(request));
      //  app.MapDelete("/customers/{id}", async (IMediator mediator, Guid id) => await mediator.Send(new DeleteCustomerByIdRequest(id)));
        // app.MapGet("/customers", GetAllCustomers);
        // app.MapGet("/customers/{id}", GetCustomer);
        // app.MapPost("/customers", CreateCustomer);
        // app.MapPut("/customers", UpdateCustomer);
        // app.MapDelete("/customers/{id}", DeleteCustomer);
    }

    private static Task GetAllCustomers(IMediator mediator)
    {
        throw new NotImplementedException();
    }
    private static async Task<IResult> GetCustomer(IMediator mediator, Guid id, GetCustomer.Query query)
    {
        var customer = await mediator.Send(query);
        return customer is not null? Results.Ok(customer): Results.NotFound();
    } 
    private static async Task<IResult> AddCustomer(IMediator mediator, CreateCustomer.Command command)
    {
         await mediator.Send(command);
         return Results.Ok();
    } 
    private static Task UpdateCustomer(IMediator mediator)
    {
        throw new NotImplementedException();
    }
    private static Task DeleteCustomer(IMediator mediator)
    {
        if (mediator == null) throw new ArgumentNullException(nameof(mediator));
        throw new NotImplementedException();
    }
}