using Microsoft.Extensions.Configuration;
using SecretsManagement.WebApi;

var builder = WebApplication.CreateBuilder(args);

var movieApiKey = builder.Configuration["Movies:ServiceApiKey"];


var moviesConfig = builder.Configuration.GetSection(MovieSettings.SectionName).Get<MovieSettings>();
var _moviesApiKey = moviesConfig.ServiceApiKey;

builder.Services.Configure<MovieSettings>(builder.Configuration.GetSection(MovieSettings.SectionName));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
