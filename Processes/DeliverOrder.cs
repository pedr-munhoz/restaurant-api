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

        if (entity.DeliveryLocation is null || entity.DeliveryLocation == "")
        {
            entity.Delivered = true;
            entity.Status = OperationStatus.Succeeded;

            await _dbContext.SaveChangesAsync();
            return;
        }

        Uri? uriResult;
        bool result = Uri.TryCreate(entity.DeliveryLocation, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (!result || uriResult is null)
        {
            entity.Delivered = true;
            entity.Status = OperationStatus.Failed;

            await _dbContext.SaveChangesAsync();
            return;
        }

        var response = await _httpClient.PostAsJsonAsync(uriResult, entity);
        entity.DeliveryResponse = (int)response.StatusCode;

        entity.Delivered = true;
        entity.Status = response.IsSuccessStatusCode
            ? OperationStatus.Succeeded
            : OperationStatus.Failed;

        await _dbContext.SaveChangesAsync();
    }
}
