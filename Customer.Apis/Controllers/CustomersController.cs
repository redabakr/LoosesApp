using Customer.Application.Commands;
using Customer.Application.DTO;
using Customer.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    //private readonly IMediator _mediator;
    public CustomersController(ILogger<CustomersController> logger)
    {
        _logger = logger;
        
    }


    [HttpGet("id:guid")]
    public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer query)
    {
       // var response = await _mediator.Send(query);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomer command)
    {
       // var response = await _mediator.Send(command);
        return Ok();
    }
}