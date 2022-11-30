using MassTransit;
using Microsoft.EntityFrameworkCore;
using restaurant_api.Infrastructure.Database;
using restaurant_api.Models.Enums;
using restaurant_api.Models.ProcessRequests;

namespace restaurant_api.Processes;

public class DeliverOrder : IConsumer<DeliverOrderRequest>
{
    private readonly RestaurantDbContext _dbContext;
    private readonly HttpClient _httpClient;

    public DeliverOrder(RestaurantDbContext dbContext, HttpClient httpClient)
    {
        _dbContext = dbContext;
        _httpClient = httpClient;
    }

    public async Task Consume(ConsumeContext<DeliverOrderRequest> context)
    {
        var entity = await _dbContext.Orders
            .Where(x => x.Id == context.Message.OrderId)
            .FirstOrDefaultAsync();

        if (entity is null || entity.Delivered)
            return;

        if (entity.DeliveryLocation is not null && entity.DeliveryLocation != "")
        {
            var response = await _httpClient.PostAsJsonAsync(entity.DeliveryLocation, entity);
            entity.DeliveryResponse = (int)response.StatusCode;
        }

        entity.Delivered = true;
        entity.Status = OperationStatus.Succeeded;

        await _dbContext.SaveChangesAsync();
    }
}
