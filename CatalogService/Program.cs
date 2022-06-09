using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNotificationHandler();

builder.Services.AddHttpClients(builder.Configuration.GetSection("Services"));
builder.Logging.AddSeq(builder.Configuration["Seq"]);

var app = builder.Build();

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

app.MapControllers();

app.Run();
