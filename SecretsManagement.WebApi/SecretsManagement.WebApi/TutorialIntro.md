where the secrete values saved
==============================
The values are stored in a JSON file in the local machine's user profile folder

    %APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json

In the preceding file paths, replace <user_secrets_id> with the UserSecretsId value specified in the project file.

Enable secret storage
=====================
The Secret Manager tool includes an init command. To use user secrets, run the following command in the project directory

    dotnet user-secrets init

The preceding command adds a UserSecretsId element within a PropertyGroup of the project file. By default, the inner text of UserSecretsId is a GUID. The inner text is arbitrary, but is unique to the project.

    <PropertyGroup>
      <TargetFramework>netcoreapp3.1</TargetFramework>
      <UserSecretsId>79a3edd0-2092-40a2-a04d-dcb46d5ca9ed</UserSecretsId>
    </PropertyGroup>

Set a secret
============

    dotnet user-secrets set "Movies:ServiceApiKey" "12345"

JSON structure flattening in Visual Studio
==========================================
Visual Studio's Manage User Secrets gesture opens a secrets.json file in the text editor

![Manage user Secretes](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets/_static/usvs.png?view=aspnetcore-6.0)

    {
      "Movies": {
      "ConnectionString": "Server=(localdb)\\mssqllocaldb;Database=Movie-1;Trusted_Connection=True;MultipleActiveResultSets=true",
      "ServiceApiKey": "12345"
        }
      }

Access a secret
===============

Register the user secrets configuration source
----------------------------------------------

    var builder = WebApplication.CreateBuilder(args);
    var app = builder.Build();
    
    app.MapGet("/", () => "Hello World!");
    
    app.Run();

Read the secret via the Configuration API
-----------------------------------------

    var builder = WebApplication.CreateBuilder(args);
    var movieApiKey = builder.Configuration["Movies:ServiceApiKey"];
    
    var app = builder.Build();
    
    app.MapGet("/", () => movieApiKey);
    
    app.Run();

Map secrets to a POCO
---------------------
Assume the app's secrets.json file contains the following two secrets:

    {
      "Movies:ConnectionString": "Server=(localdb)\\mssqllocaldb;Database=Movie-1;Trusted_Connection=True;MultipleActiveResultSets=true",
      "Movies:ServiceApiKey": "12345"
    }

To map the preceding secrets to a POCO

    public class MovieSettings
    {
        public string ConnectionString { get; set; }
    
        public string ServiceApiKey { get; set; }
    }

----------


    var moviesConfig = Configuration.GetSection("Movies").Get<MovieSettings>();
    _moviesApiKey = moviesConfig.ServiceApiKey;

String replacement with secrets
===============================

Storing passwords in plain text is insecure. For example, a database connection string stored in appsettings.json may include a password for the specified user:

    {
    "ConnectionStrings": {
        "Movies": "Server=(localdb)\\mssqllocaldb;Database=Movie-1;User Id=johndoe;Password=pass123;MultipleActiveResultSets=true"
        }
    }
A more secure approach is to store the password as a secret. For example:

    dotnet user-secrets set "DbPassword" "pass123"

Remove the Password key-value pair from the connection string in appsettings.json. For example:

    {
      "ConnectionStrings": {"Movies": "Server=(localdb)\\mssqllocaldb;Database=Movie-1;User Id=johndoe;MultipleActiveResultSets=true"
      }
    }

The secret's value can be set on a SqlConnectionStringBuilder object's Password property to complete the connection string:

    using System.Data.SqlClient;
    
    var builder = WebApplication.CreateBuilder(args);
    
    var conStrBuilder = new SqlConnectionStringBuilder(
    builder.Configuration.GetConnectionString("Movies"));
    conStrBuilder.Password = builder.Configuration["DbPassword"];
    var connection = conStrBuilder.ConnectionString;
    
    var app = builder.Build();
    
    app.MapGet("/", () => connection);
    
    app.Run();


Remove a single secret
======================

    dotnet user-secrets remove "Movies:ConnectionString"


Remove all secrets
==================

    dotnet user-secrets clear

