using Microsoft.AspNetCore.Mvc;
using restaurant_api.Infrastructure.Database;
using restaurant_api.Models.Entities;
using restaurant_api.Models.ViewModels;

namespace restaurant_api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly RestaurantDbContext _dbContext;

    public OrderController(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost, Route("")]
    public async Task<IActionResult> MakeOrder([FromBody] OrderViewModel viewModel)
    {
        var entity = new Order
        {
            Burgers = viewModel.Burgers,
            Fries = viewModel.Fries,
            Sodas = viewModel.Sodas,
            BurgersReady = viewModel.Burgers <= 0,
            FriesReady = viewModel.Fries <= 0,
            SodasReady = viewModel.Sodas <= 0,
        };

        await _dbContext.Orders.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return Ok($"Your order will be prepared, order id = {entity.Id}");
    }
}
