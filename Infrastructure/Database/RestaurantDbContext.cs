using Microsoft.EntityFrameworkCore;
using restaurant_api.Models.Entities;

namespace restaurant_api.Infrastructure.Database;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
}
