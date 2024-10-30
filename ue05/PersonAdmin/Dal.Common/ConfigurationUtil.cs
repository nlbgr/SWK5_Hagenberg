using Microsoft.Extensions.Configuration;

namespace Dal.Common;

public static class ConfigurationUtil
{
  private static IConfiguration? configuration = null;

  public static IConfiguration GetConfiguration() =>
    configuration ??= new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", optional: false)
      .Build();

  public static (string ConnectionString, string ProviderName) GetConnectionParameters(string connectionConfigName, string providerConfigName)
  {
    return GetConnectionParameters(GetConfiguration(), connectionConfigName, providerConfigName);
  }

  public static (string ConnectionString, string ProviderName) GetConnectionParameters(IConfiguration configuration, string connectionConfigName, string providerConfigName)
  {
    var connectionString = configuration.GetConnectionString(connectionConfigName);
    if (connectionString is null)
    {
      throw new ArgumentException($"Connection string with key '{connectionConfigName}' does not exist");
    }


    var providerName = configuration[providerConfigName];
    if (providerName is null)
    {
      throw new ArgumentException($"Configuration property '{providerConfigName}' does not exist");
    }

    return (connectionString, providerName);
  }
}
