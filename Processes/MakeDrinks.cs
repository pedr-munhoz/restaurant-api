using MassTransit;
using restaurant_api.Models.ProcessRequests;

namespace restaurant_api.Processes;

public class MakeDrinks : IConsumer<MakeDrinksRequest>
{
    private readonly IBusControl _bus;

    public MakeDrinks(IBusControl bus)
    {
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<MakeDrinksRequest> context)
    {
        await Task.Delay(3000 * context.Message.Drinks);
        await _bus.Publish(
            new AssembleOrderRequest
            {
                OrderId = context.Message.OrderId,
                SodasReady = true,
            });
    }
}
