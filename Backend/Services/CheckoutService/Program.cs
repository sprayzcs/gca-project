using System.Reflection;
using CheckoutService.Data;
using CheckoutService.Infrastructure;
using CheckoutService.Model;
using CheckoutService.Services;
using Shared;
using Shared.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithXML(Assembly.GetExecutingAssembly().GetName().Name);

builder.Services.AddDatabaseContext<CheckoutContext>(builder.Configuration["ConnectionString"]);
builder.Services.AddNotificationHandler();
builder.Services.AddSecurityServices(builder.Configuration);
builder.Services.AddHttpClients(builder.Configuration.GetSection("Services"));
builder.Services.AddDefaultCors(builder.Configuration);
builder.Logging.AddSeq(builder.Configuration["Seq"]);

builder.Services.AddAutoMapper(typeof(Order), typeof(OrderDto));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICheckoutService, CheckoutService.Services.CheckoutService>();

var app = builder.Build();

await app.MigrateDbContext<CheckoutContext>();

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
