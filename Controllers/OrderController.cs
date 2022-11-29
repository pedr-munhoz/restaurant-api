using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant_api.Infrastructure.Database;
using restaurant_api.Models.Entities;
using restaurant_api.Models.ProcessRequests;
using restaurant_api.Models.ViewModels;

namespace restaurant_api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IBusControl _bus;

    public OrderController(RestaurantDbContext dbContext, IBusControl bus)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
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

        var makeOrderRequest = new MakeOrderRequest { OrderId = entity.Id };
        await _bus.Publish(makeOrderRequest);

        return Ok($"Your order will be prepared, order id = {entity.Id}");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetOrder([FromRoute] int id)
    {
        var entity = await _dbContext.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (entity is null)
            return NotFound();

        return Ok(entity);
    }
}
