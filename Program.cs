using System.Reflection;
using YoutubeExplode;
using Microsoft.OpenApi.Models;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "YouTube API",
        Version = "v1",
        Description = "Test API for YouTube using YoutubeExplode",
    });
});

builder.Services.AddMediatR(typeof(Program).Assembly);

// YouTube Client
builder.Services.AddSingleton<YoutubeClient>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();