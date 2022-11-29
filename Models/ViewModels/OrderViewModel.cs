using System.ComponentModel.DataAnnotations;

namespace restaurant_api.Models.ViewModels;

public class OrderViewModel
{
    public string? DeliveryLocation { get; set; }

    [Range(0, 5)]
    public int Burgers { get; set; }

    [Range(0, 10)]
    public int Fries { get; set; }

    [Range(0, 20)]
    public int Sodas { get; set; }
}
