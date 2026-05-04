using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherApiExercise.Handlers;

namespace WeatherApiExercise.Controllers;

[ApiController]

[Route("api/[controller]")]
public class WeatherController: ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpGet("{name}")]
    public async Task<IActionResult> Get(string name)
    {
        var result = await _mediator.Send(new GetWeatherByCityQuery(name));
        if( result == null) return NotFound();
        return Ok(result);
    }
}