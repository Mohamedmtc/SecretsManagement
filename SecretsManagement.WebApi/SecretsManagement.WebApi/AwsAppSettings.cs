namespace SecretsManagement.WebApi
{
    public class AwsAppSettings
    {
        public static string SectionName { get; set; } = "AwsSettings";
        public string Region { get; set; }
        public string UserAccessKeyId { get; set; }
        public string UserAccessSecretKey { get; set; }
    }
}
