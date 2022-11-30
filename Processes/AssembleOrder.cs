using MassTransit;
using Microsoft.EntityFrameworkCore;
using restaurant_api.Infrastructure.Database;
using restaurant_api.Models.ProcessRequests;

namespace restaurant_api.Processes;

public class AssembleOrder : IConsumer<AssembleOrderRequest>
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IBusControl _bus;

    public AssembleOrder(RestaurantDbContext dbContext, IBusControl bus)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<AssembleOrderRequest> context)
    {
        var entity = await _dbContext.Orders
            .Where(x => x.Id == context.Message.OrderId)
            .FirstOrDefaultAsync();

        if (entity is null)
            return;

        if (context.Message.BurgersReady)
            entity.BurgersReady = true;

        if (context.Message.FriesReady)
            entity.FriesReady = true;

        if (context.Message.SodasReady)
            entity.SodasReady = true;

        await _dbContext.SaveChangesAsync();

        _dbContext.Entry(entity).Reload();

        if (entity.isReady && !entity.DeliveryRequested)
        {
            entity.DeliveryRequested = true;
            await _dbContext.SaveChangesAsync();

            var deliveryRequest = new DeliverOrderRequest { OrderId = entity.Id };
            await _bus.Publish(deliveryRequest);
        }
    }
}
