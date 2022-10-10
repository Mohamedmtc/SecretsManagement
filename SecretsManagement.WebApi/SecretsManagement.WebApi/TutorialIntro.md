.Net Configurations
===================

Application configuration in ASP.NET Core is performed using one or more configuration providers. Configuration providers read configuration data from key-value pairs using a variety of configuration sources

ASP.NET Core apps configure and launch a host. The host is responsible for app startup and lifetime management. The ASP.NET Core templates create a WebApplicationBuilder which contains the host. While some configuration can be done in both the host and the application configuration providers, generally, only configuration that is necessary for the host should be done in host configuration.

Application configuration is the highest priority and is detailed in the next section. Host configuration follows application configuration, and is described in this article.

Host configuration
==================

The following list contains the default host configuration sources from highest to lowest priority:

    ASPNETCORE_-prefixed environment variables using the Environment variables configuration provider.
    Command-line arguments using the Command-line configuration provider
    DOTNET_-prefixed environment variables using the Environment variables configuration provider.

When a configuration value is set in host and application configuration, the application configuration is used

Host variables
==============

The following variables are locked in early when initializing the host builders and can't be influenced by application config:

    Application name
    Environment name, for example Development, Production, and Staging
    Content root
    Web root

