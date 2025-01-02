using Microsoft.AspNetCore.Mvc;

namespace TotalOne.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PingController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public string Get()
    {
        return "pong";
    }
}