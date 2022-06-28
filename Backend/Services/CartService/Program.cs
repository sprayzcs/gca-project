using CartService.Data;
using CartService.Infrastructure;
using CartService.Model;
using CartService.Services;
using Shared;
using Shared.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = builder.Configuration.GetValue<TimeSpan>("SessionTimeout");
});

builder.Services.AddDatabaseContext<CartContext>(builder.Configuration["ConnectionString"], "cart");
builder.Services.AddNotificationHandler();
builder.Services.AddSecurityServices(builder.Configuration);
builder.Services.AddHttpClients(builder.Configuration.GetSection("Services"));
builder.Logging.AddSeq(builder.Configuration["Seq"]);

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<Cart, CartDto>();
}, typeof(Cart), typeof(CartDto));

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService.Services.CartService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

await app.MigrateDbContext<CartContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

if (!builder.Environment.IsDevelopment())
{
    app.UseCustomLag();
}

app.UseSession();

app.MapControllers();

app.Run();
