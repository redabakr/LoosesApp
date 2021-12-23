using Customer.Application.Commands;
using Customer.Application.DTO;
using Customer.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.APII.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
     
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name ="GetCustomer")]
   // [Route("{id}")]
    public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer.Query query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost(Name="CreateCustomer")]
    public async Task<IActionResult> Post([FromBody] CreateCustomer.Command command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
    
    // [HttpGet(Name = "GetWeatherForecast")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //         {
    //             Date = DateTime.Now.AddDays(index),
    //             TemperatureC = Random.Shared.Next(-20, 55)
    //         })
    //         .ToArray();
    // }
}