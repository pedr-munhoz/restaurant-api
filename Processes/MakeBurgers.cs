using MassTransit;
using restaurant_api.Models.ProcessRequests;

namespace restaurant_api.Processes;

public class MakeBurgers : IConsumer<MakeBurgersRequest>
{
    private readonly IBusControl _bus;

    public MakeBurgers(IBusControl bus)
    {
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<MakeBurgersRequest> context)
    {
        var assembleRequest = new AssembleOrderRequest
        {
            OrderId = context.Message.OrderId,
            BurgersReady = true,
        };

        await _bus.Publish(assembleRequest);
    }
}
