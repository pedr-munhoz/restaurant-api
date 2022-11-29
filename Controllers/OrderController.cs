using Microsoft.AspNetCore.Mvc;
using restaurant_api.Models.Entities;
using restaurant_api.Models.ViewModels;

namespace restaurant_api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    [HttpPost, Route("")]
    public async Task<IActionResult> MakeOrder([FromBody] OrderViewModel viewModel)
    {
        await Task.CompletedTask;

        var entity = new Order
        {
            Burgers = viewModel.Burgers,
            Fries = viewModel.Fries,
            Sodas = viewModel.Sodas,
            BurgersReady = viewModel.Burgers <= 0,
            FriesReady = viewModel.Fries <= 0,
            SodasReady = viewModel.Sodas <= 0,
        };

        return Ok($"Your order will be prepared, order id = {entity.Id}");
    }
}
