namespace restaurant_api.Models.Entities;

public class Order
{
    public int Id { get; set; }
    public int Burgers { get; set; }
    public int Fries { get; set; }
    public int Sodas { get; set; }
    public bool KitchenAck { get; set; }
    public bool BurgersReady { get; set; }
    public bool FriesReady { get; set; }
    public bool SodasReady { get; set; }
    public bool Delivered { get; set; }
}