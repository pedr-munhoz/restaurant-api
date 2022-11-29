using MassTransit;
using Microsoft.EntityFrameworkCore;
using restaurant_api.Infrastructure.Database;
using restaurant_api.Models.ProcessRequests;

namespace restaurant_api.Processes;

public class MakeOrder : IConsumer<MakeOrderRequest>
{
    private readonly RestaurantDbContext _dbContext;

    public MakeOrder(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<MakeOrderRequest> context)
    {
        var entity = await _dbContext.Orders.Where(x => x.Id == context.Message.OrderId).FirstOrDefaultAsync();

        if (entity is null)
            return;

        entity.KitchenAck = true;

        await _dbContext.SaveChangesAsync();
    }
}
