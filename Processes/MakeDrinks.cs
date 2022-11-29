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
        var assembleRequest = new AssembleOrderRequest
        {
            OrderId = context.Message.OrderId,
            SodasReady = true,
        };

        await _bus.Publish(assembleRequest);
    }
}
