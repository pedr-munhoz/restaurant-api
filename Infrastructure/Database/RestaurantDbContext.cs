using Microsoft.EntityFrameworkCore;

namespace restaurant_api.Infrastructure.Database;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions options) : base(options)
    {
    }
}
