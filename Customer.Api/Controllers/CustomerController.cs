using Customer.Application.Commands;
using Customer.Application.DTO;
using Customer.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
namespace Customer.Api.Controllers;

public class CustomersController: BaseController
{
    private readonly IMediator _mediator;
    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer.Query query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomer.Command command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}