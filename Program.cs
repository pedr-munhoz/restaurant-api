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
    x.AddConsumer<MakeBurgers>();
    x.AddConsumer<MakeFries>();
    x.AddConsumer<MakeDrinks>();
    x.AddConsumer<AssembleOrder>();
    x.AddConsumer<DeliverOrder>();

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

        cfg.ReceiveEndpoint("make-burgers-queue", e =>
        {
            e.ConfigureConsumer<MakeBurgers>(context);
        });

        cfg.ReceiveEndpoint("make-fries-queue", e =>
        {
            e.ConfigureConsumer<MakeFries>(context);
        });

        cfg.ReceiveEndpoint("make-drinks-queue", e =>
        {
            e.ConfigureConsumer<MakeDrinks>(context);
        });

        cfg.ReceiveEndpoint("assemble-order-queue", e =>
        {
            e.ConfigureConsumer<AssembleOrder>(context);
        });

        cfg.ReceiveEndpoint("deliver-order-queue", e =>
        {
            e.ConfigureConsumer<DeliverOrder>(context);
        });
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<DeliverOrder>();

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
