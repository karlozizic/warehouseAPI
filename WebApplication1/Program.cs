using System.Net;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Interfaces;
using WebApplication1.Repositories;
using WebApplication1.Services;
using X.Auth.Middleware.Extensions;
using X.Consul.Helpers;
using X.LogMiddleware.Extensions;
using X.PortUtility;

var builder = WebApplication.CreateBuilder(args);

var bindingConfig = new ConfigurationBuilder().AddCommandLine(args).Build();

var port = bindingConfig.GetValue<int?>("port") ?? FreePorts.Find();
builder.Services.AddCors();
builder.Services.AddOptions(); 
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddMemoryCache(); 
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Any, port);
});

//WarehouseContext
builder.Services.AddDbContext<WarehouseContext>();

//Potrebno je autowireati Interface i realizaciju Interfacea
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>(); 
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();

//
builder.Services.AddLogWriter(builder.Configuration);
builder.Services.AddAuthMiddleware(builder.Configuration);
builder.Services.AddRequestService();
builder.Services.AddConsul(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}   

app.UseRouting();
app.UseRefreshAuthMiddleware();
app.UseAuthorization();
app.MapControllers();


app.Run();