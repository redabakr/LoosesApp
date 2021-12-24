using Customer.Application.Commands;
using Customer.Application.Queries;
using MediatR;

namespace Customer.Api.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
       // app.MapGet("customers", async (IMediator mediator) => await mediator.Send(new GetAllCustomersRequest()));
        app.MapGet("/customers", async (IMediator mediator, Guid id) => await mediator.Send(new GetCustomers.Query("")));
        app.MapGet("/customers/{id}", async (IMediator mediator, Guid id) => await mediator.Send(new GetCustomer.Query(id)));
        app.MapPost("/customers", async (IMediator mediator, CreateCustomer.Command request) => await mediator.Send(request));
        app.MapPost("/customers/{id}/shipping-address", async (IMediator mediator, AddCustomerShippingAddress.Command request) => await mediator.Send(request));
        app.MapDelete("/customers/{id}", async (IMediator mediator, Guid id) => await mediator.Send(new RemoveCustomer.Command(id)));
        app.MapDelete("/customers/{id}/shipping-address/{shippingAddressName}", async (IMediator mediator, Guid id, string shippingAddressName) => await mediator.Send(new RemoveCustomerShippingAddress.Command(id, shippingAddressName)));
        app.MapPut("/customers/{id}/shipping-address/{shippingAddressName}", async (IMediator mediator, Guid id, string shippingAddressName)=> await  mediator.Send(new SetDefaultCustomerShippingAddress.Command(id, shippingAddressName) ));
        app.MapPut("/customers/{id}", async (IMediator mediator, UpdateCustomer.Command request)=> await  mediator.Send(request ));
    }
}