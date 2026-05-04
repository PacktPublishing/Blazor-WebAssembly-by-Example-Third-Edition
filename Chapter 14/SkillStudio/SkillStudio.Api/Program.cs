using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using SkillStudio.Api.Interfaces;
using SkillStudio.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSkillStudioClient", policy =>
    {
        policy.WithOrigins("https://localhost:7238")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSingleton(sp =>
{
    var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

    var apiKey = configuration["OpenAI:ApiKey"]
        ?? throw new InvalidOperationException("Missing OpenAI API.");

    return new ChatClient("gpt-5.4", apiKey);
});

builder.Services.AddScoped<IOpenAiService, OpenAiService>();

builder.Services.AddSingleton<ISkillParser, SkillParser>();
builder.Services.AddSingleton<ISkillRepo, SkillRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("AllowSkillStudioClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
