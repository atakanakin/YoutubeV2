using System.Text.Json.Serialization;
using YoutubeExplode;
using Microsoft.OpenApi.Models;
using MediatR;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

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
builder.Services.AddSingleton<YoutubeClient>(sp =>
{
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
    return new YoutubeClient(httpClient);
});

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://youtube-v2-nu.vercel.app")
                  .WithHeaders("Content-Type", "Authorization")
                  .WithMethods("GET", "POST", "OPTIONS");
            //   .AllowCredentials(); 
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run("http://0.0.0.0:5000");