using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NotionNotifications.Domain;

namespace NotionNotifications.PWA
{
    public static class Configuration
    {
        public static void ConfigureServices(
            this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress), });

            builder.Services.AddScoped<INotificationHandler, PwaNotificationHandler>();

            builder.Services.AddScoped<NotionNotificationsPwaClientHub>();

            builder.Services.AddScoped<NotificationCenterService>();
        }
    }
}
