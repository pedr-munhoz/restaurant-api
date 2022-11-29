using MassTransit;
using Microsoft.EntityFrameworkCore;
using restaurant_api.Infrastructure.Database;
using restaurant_api.Processes;
using restaurant_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration["RestaurantDbConnectionString"];

builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseNpgsql(connectionString)
);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MakeOrder>();
    x.AddConsumer<MakeDrink>();
    x.AddConsumer<AssembleOrder>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("make-order-queue", e =>
        {
            e.ConfigureConsumer<MakeOrder>(context);
        });

        cfg.ReceiveEndpoint("make-drink-queue", e =>
        {
            e.ConfigureConsumer<MakeDrink>(context);
        });

        cfg.ReceiveEndpoint("assemble-order-queue", e =>
        {
            e.ConfigureConsumer<AssembleOrder>(context);
        });
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

DatabaseManagementService.MigrationInitialisation(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
