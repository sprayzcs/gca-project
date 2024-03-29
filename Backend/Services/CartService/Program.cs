﻿using System.Reflection;
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
builder.Services.AddSwaggerWithXML(Assembly.GetExecutingAssembly().GetName().Name!);

builder.Services.AddDatabaseContext<CartContext>(builder.Configuration["ConnectionString"]);
builder.Services.AddNotificationHandler();
builder.Services.AddSecurityServices(builder.Configuration);
builder.Services.AddHttpClients(builder.Configuration.GetSection("Services"));
builder.Services.AddDefaultCors(builder.Configuration);
builder.Logging.AddSeq(builder.Configuration["Seq"]);

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<Cart, CartDto>()
        .ForMember(c => c.ProductIds, c => c.MapFrom(m => m.Products.Select(p => p.ProductId)));
}, typeof(Cart), typeof(CartDto));

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartProductRepository, CartProductRepository>();
builder.Services.AddScoped<ICartService, CartService.Services.CartService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

await app.MigrateDbContext<CartContext>();

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

