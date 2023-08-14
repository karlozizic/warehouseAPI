using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Database;
using WebApplication1.Hubs;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;
using X.Auth.Middleware.Extensions;
using X.Consul.Helpers;
using X.LogMiddleware.Extensions;
using X.PortUtility;

var builder = WebApplication.CreateBuilder(args);

var bindingConfig = new ConfigurationBuilder().AddCommandLine(args).Build();

var port = bindingConfig.GetValue<int?>("port") ?? FreePorts.Find();
// sljedeci kod se inace ne konfigurira unutar Program.cs
builder.Services.AddCors();
//
builder.Services.AddOptions();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// sljedeci kod se inace ne konfigurira unutar Program.cs
builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Any, port);
});
//
// DbContext
/*builder.Services.AddDbContext<WarehouseContext>();*/

builder.Services.Configure<AppSettingsModel>(builder.Configuration.GetSection("AppSettings"));

// Potrebno je autowireati Interface i realizaciju Interfacea
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>(); 
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IRetailService, RetailService>();
builder.Services.AddScoped<IFranchiseUserRepository, FranchiseUserRepository>();
builder.Services.AddScoped<IFranchiseUserService, FranchiseUserService>();
builder.Services.AddScoped<IItemRequestRepository, ItemRequestRepository>();
builder.Services.AddScoped<IItemRequestService, ItemRequestService>();

builder.Services.AddScoped<IContextService, ContextService>();

//
builder.Services.AddLogWriter(builder.Configuration);
builder.Services.AddAuthMiddleware(builder.Configuration);
builder.Services.AddRequestService();
builder.Services.AddConsul(builder.Configuration);
//

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = "https://authorization.stage-volcano.com"
    };
});

builder.Services.AddHttpClient(); 

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
app.UseAuthentication();

app.MapHub<NotificationHub>("/hub");

app.Run();