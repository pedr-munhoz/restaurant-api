using MassTransit;
using restaurant_api.Models.ProcessRequests;

namespace restaurant_api.Processes;

public class MakeDrink : IConsumer<MakeDrinkRequest>
{
    private readonly IBusControl _bus;

    public MakeDrink(IBusControl bus)
    {
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<MakeDrinkRequest> context)
    {
        var assembleRequest = new AssembleOrderRequest
        {
            OrderId = context.Message.OrderId,
            SodasReady = true,
        };

        await _bus.Publish(assembleRequest);
    }
}
