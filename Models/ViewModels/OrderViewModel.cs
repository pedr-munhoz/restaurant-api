using System.ComponentModel.DataAnnotations;

namespace restaurant_api.Models.ViewModels;

public class OrderViewModel
{
    [Range(0, 5)]
    public int Burgers { get; set; }

    [Range(0, 10)]
    public int Fries { get; set; }

    [Range(0, 20)]
    public int Sodas { get; set; }
}
