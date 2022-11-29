using Microsoft.EntityFrameworkCore;
using restaurant_api.Infrastructure.Database;

namespace restaurant_api.Services;

public class DatabaseManagementService
{
    public static void MigrationInitialisation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            serviceScope.ServiceProvider.GetService<RestaurantDbContext>()?.Database.Migrate();
        }
    }
}
