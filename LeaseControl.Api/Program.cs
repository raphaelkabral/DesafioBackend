using LeaseControl.Domain.InterfaceRepository;
using LeaseControl.Infrastructure;
using LeaseControl.Infrastructure.Mensageria;
using LeaseControl.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
builder.Services.AddScoped<ILeaserRepository, LeaserRepository>();
builder.Services.AddSingleton<MotorcycleNotifier>();
builder.Services.AddHostedService<MotorcycleConsumer>();

builder.Services.AddSingleton<IModel>(provider =>
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    var connection = factory.CreateConnection();
    return connection.CreateModel();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
