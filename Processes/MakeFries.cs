using MassTransit;
using restaurant_api.Models.ProcessRequests;

namespace restaurant_api.Processes;

public class MakeFries : IConsumer<MakeFriesRequest>
{
    private readonly IBusControl _bus;

    public MakeFries(IBusControl bus)
    {
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<MakeFriesRequest> context)
    {
        await _bus.Publish(
            new AssembleOrderRequest
            {
                OrderId = context.Message.OrderId,
                FriesReady = true,
            });
    }
}
