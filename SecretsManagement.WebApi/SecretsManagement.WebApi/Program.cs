using Amazon;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using SecretsManagement.WebApi;

var builder = WebApplication.CreateBuilder(args);



var awsAppSettings = builder.Configuration.GetSection(AwsAppSettings.SectionName).Get<AwsAppSettings>();

var env = builder.Environment.EnvironmentName;

builder.Configuration.AddSecretsManager(
        credentials: new BasicAWSCredentials(awsAppSettings.UserAccessKeyId, awsAppSettings.UserAccessSecretKey),
        region: RegionEndpoint.GetBySystemName(awsAppSettings.Region),
        configurator: options =>
        {

            options.SecretFilter = entry => entry.Name.ToLower().StartsWith($"{env}_Secrete.WebApi".ToLower());
            options.KeyGenerator = (_, s) => s
                .Remove(0, s.IndexOf(':') + 1)
                .Replace("__", ":");
            options.PollingInterval = TimeSpan.FromSeconds(10);
        });

var movieSettings = builder.Configuration.GetSection(MovieSettings.SectionName).Get<MovieSettings>();

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
