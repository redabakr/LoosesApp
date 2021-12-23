using Customer.Application.Commands;
using Customer.Application.DTO;
using Customer.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
namespace Customer.Api.Controllers;

public class CustomerController: BaseController
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("id:guid")]
    public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomer command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}