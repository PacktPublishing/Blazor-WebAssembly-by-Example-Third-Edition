using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Api.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaskManagerApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerApiContext") ?? throw new InvalidOperationException("Connection string 'TaskManagerApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowTaskManagerClient", policy =>
    {
        policy.WithOrigins("https://localhost:7036")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("AllowTaskManagerClient");
app.UseAuthorization();

app.MapControllers();

app.Run();
