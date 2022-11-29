namespace restaurant_api.Models.ProcessRequests;

public class AssembleOrderRequest
{
    public int OrderId { get; set; }
    public bool BurgersReady { get; set; }
    public bool FriesReady { get; set; }
    public bool SodasReady { get; set; }
}
