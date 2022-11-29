using MassTransit;
using Microsoft.EntityFrameworkCore;
using restaurant_api.Infrastructure.Database;
using restaurant_api.Models.ProcessRequests;

namespace restaurant_api.Processes;

public class MakeOrder : IConsumer<MakeOrderRequest>
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IBusControl _bus;

    public MakeOrder(RestaurantDbContext dbContext, IBusControl bus)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<MakeOrderRequest> context)
    {
        var entity = await _dbContext.Orders
            .Where(x => x.Id == context.Message.OrderId)
            .FirstOrDefaultAsync();

        if (entity is null)
            return;

        entity.KitchenAck = true;
        await _dbContext.SaveChangesAsync();

        var burgersRequest = new MakeBurgersRequest
        {
            OrderId = entity.Id,
            Burgers = entity.Sodas,
        };

        await _bus.Publish(burgersRequest);

        var drinkRequest = new MakeDrinksRequest
        {
            OrderId = entity.Id,
            Drinks = entity.Sodas,
        };

        await _bus.Publish(drinkRequest);
    }
}
