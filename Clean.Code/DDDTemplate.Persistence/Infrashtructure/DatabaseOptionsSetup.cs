using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DDDTemplate.Persistence.Infrashtructure;

public class DatabaseOptionsSetup(IConfiguration configuration) : IConfigureOptions<DatabaseOptions>
{
    private const string SettingsKey = "Database";
    private const string ConfigurationSectionName = "DatabaseOptions";

    public void Configure(DatabaseOptions options)
    {
        options.ConnectionString = configuration.GetConnectionString(SettingsKey)!;
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}