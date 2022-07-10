using System.Reflection;
using Shared;
using Shared.Data;
using ShippingService.Data;
using ShippingService.Infrastructure;
using ShippingService.Models;
using ShippingService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddNotificationHandler();
builder.Services.AddDatabaseContext<ShipmentContext>(builder.Configuration["ConnectionString"]);
builder.Services.AddSecurityServices(builder.Configuration);
builder.Services.AddHttpClients(builder.Configuration.GetSection("Services"));
builder.Services.AddDefaultCors(builder.Configuration);
builder.Logging.AddSeq(builder.Configuration["Seq"]);

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<Shipment, ShipmentDto>();
}, typeof(Shipment), typeof(ShipmentDto));

builder.Services.AddScoped<IShippingService, ShippingService.Services.ShippingService>();
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();

var app = builder.Build();

await app.MigrateDbContext<ShipmentContext>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("cors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.AddIdentityMiddleware();
if (!builder.Environment.IsDevelopment())
{
    app.UseCustomLag();
}

app.MapControllers();

app.Run();
