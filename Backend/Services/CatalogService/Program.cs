using System.Reflection;
using CatalogService.Data;
using CatalogService.Infrastructure;
using CatalogService.Model;
using CatalogService.Services;
using Shared;
using Shared.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithXML(Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.AddHttpContextAccessor();

builder.Services.AddDatabaseContext<CatalogContext>(builder.Configuration["ConnectionString"]);
builder.Services.AddNotificationHandler();
builder.Services.AddSecurityServices(builder.Configuration);
builder.Services.AddHttpClients(builder.Configuration.GetSection("Services"));
builder.Logging.AddSeq(builder.Configuration["Seq"]);

builder.Services.AddAutoMapper(typeof(Product), typeof(ProductDto));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDefaultCors(builder.Configuration);

var app = builder.Build();

await app.MigrateDbContext<CatalogContext>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("cors");
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions());

app.AddIdentityMiddleware();
if (!builder.Environment.IsDevelopment())
{
    app.UseCustomLag();
}

app.MapControllers();

app.Run();
