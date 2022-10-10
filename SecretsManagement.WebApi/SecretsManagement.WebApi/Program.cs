using Microsoft.Extensions.Configuration;
using SecretsManagement.WebApi;

var builder = WebApplication.CreateBuilder(args);


var movieApiKey = builder.Configuration["Logging:LogLevel:Default"];


var moviesConfig = builder.Configuration.GetSection("Movies").Get<MovieSettings>();
var objmoviesApiKey = moviesConfig.ServiceApiKey;

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
