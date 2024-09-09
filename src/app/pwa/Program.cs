using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NotionNotifications;
using NotionNotifications.PWA;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress), });

builder.Services.AddHttpClient<PwaSubscriptionsClient>(
        client =>
        {
            client.BaseAddress = new Uri("http://localhost:8000");
        });

builder.Services.AddSingleton<PwaNotificationHandler>();

builder.Services.AddScoped<NotionNotificationsClientHub>();

await builder.Build().RunAsync();
