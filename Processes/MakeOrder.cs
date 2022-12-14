using MassTransit;
using Microsoft.EntityFrameworkCore;
using restaurant_api.Infrastructure.Database;
using restaurant_api.Models.Enums;
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
        entity.Status = OperationStatus.InProgress;

        await _dbContext.SaveChangesAsync();

        await _bus.Publish(
            new MakeBurgersRequest
            {
                OrderId = entity.Id,
                Burgers = entity.Burgers,
            });

        await _bus.Publish(
            new MakeFriesRequest
            {
                OrderId = entity.Id,
                Fries = entity.Fries,
            });

        await _bus.Publish(
            new MakeDrinksRequest
            {
                OrderId = entity.Id,
                Drinks = entity.Sodas,
            });
    }
}
