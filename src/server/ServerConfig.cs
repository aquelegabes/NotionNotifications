using Hangfire;
using Hangfire.Storage.SQLite;

namespace NotionNotifications.Server;

public static class ServerConfig
{
    public static void ConfigureHangfire(
        this WebApplicationBuilder builder)
    {
        GlobalConfiguration.Configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage();
    }

    public static void MapHubs(
        this WebApplicationBuilder builder)
    {
        
    }
}