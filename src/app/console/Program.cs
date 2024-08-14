using Microsoft.Extensions.DependencyInjection;
using NotionNotifications.Integration;
using NotionNotifications.Integration.Models;
using System.Text.Json;
using NotionNotifications.Domain.Extensions;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using NotionNotifications.Domain.Entities;
using NotionNotifications.Domain;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var sp =
            new ServiceCollection()
            .AddHttpClient()
            .BuildServiceProvider();

        var httpClientFactory = sp.GetService<IHttpClientFactory>();

        using var notionClient = new NotionClient(httpClientFactory!);

        var filter = new NotionNotificationResultFilterModel()
        {
            //Title = "Teste de notificação",
            //Categories = ["Desenvolvimento de Software", "Gastos Viagem"]
        };

        var insertModel = new NotificationRoot
        {
            Title = $"Teste de Notificação {DateTimeOffset.Now:O}",
            EventDate = DateTimeOffset.Now,
            Occurence = ENotificationOccurence.None,
            Categories = ["Desenvolvimento de Software"]
        }.ToInsertNotionResultModel();

        try
        {
            var notifications = await notionClient.GetNotifications(filter);
            Console.WriteLine("[*] NotificationResult =>");
            Console.WriteLine(JsonSerializer.Serialize(notifications, new JsonSerializerOptions() { WriteIndented = true }));

            var notificationRoots = notifications.ToNotificationRoot();
            Console.WriteLine("[*] NotificationRoot =>");
            Console.WriteLine(JsonSerializer.Serialize(notificationRoots, new JsonSerializerOptions() { WriteIndented = true }));

            //await notionClient.AddNotification(insertModel);

            return 0;
        }
        catch (Exception ex)
        {
            var jsonResult = JsonNode.Parse(ex.Message)!.AsObject();
            Console.WriteLine("[*] Error =>");
            Console.WriteLine(JsonSerializer.Serialize(jsonResult, new JsonSerializerOptions() { WriteIndented = true }));
            return -1;
        }
    }
}