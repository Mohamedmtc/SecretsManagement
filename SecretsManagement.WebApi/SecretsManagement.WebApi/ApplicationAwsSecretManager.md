Aws Secret Manager
==================
 we will install the following Nuget Packages:

    AWSSDK.SecretsManager
    Kralizek.Extensions.Configuration.AWSSecretsManager

we will add our secrets as the following Video 

then we will load our secrets using the following Code

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

after that we will access the configuration keys using 

    var movieSettings = builder.Configuration.GetSection(MovieSettings.SectionName).Get<MovieSettings>();