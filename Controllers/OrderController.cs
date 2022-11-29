using Microsoft.AspNetCore.Mvc;
using restaurant_api.Models.ViewModels;

namespace restaurant_api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    [HttpPost, Route("")]
    public async Task<IActionResult> MakeOrder([FromBody] OrderViewModel order)
    {
        await Task.CompletedTask;
        return Ok("Your order will be prepared");
    }
}
