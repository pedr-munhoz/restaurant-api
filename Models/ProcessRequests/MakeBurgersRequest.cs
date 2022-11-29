namespace restaurant_api.Models.ProcessRequests;

public class MakeBurgersRequest
{
    public int OrderId { get; set; }
    public int Burgers { get; set; }
}
