AppSettings files
=================

Order of Precedence
-------------------

The last key loaded wins.


- appsettings.json file
- appsettings.{env.EnvironmentName}.json file


The appsettings.json file can be configured with Key and Value pair combinations. So, the Key will have single or multiple values.

    {  
     "ConnectionStrings": {  
        "MyDatabase": "Server=localhost;Initial Catalog=MySampleDatabase;Trusted_Connection=Yes;MultipleActiveResultSets=true"  
      }
    } 

Access appSettings
==================

Read the appSettings via the Configuration API
-----------------------------------------

    var builder = WebApplication.CreateBuilder(args);
    var movieApiKey = builder.Configuration["Movies:ServiceApiKey"];
    
    var app = builder.Build();
    
    app.MapGet("/", () => movieApiKey);
    
    app.Run();


Register in Dependency Injection
--------------------------------

in program.cs 

    builder.Services.Configure<MovieSettings>(builder.Configuration.GetSection("Movies"));

in controller

    private readonly IOptions<MovieSettings> _options;


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