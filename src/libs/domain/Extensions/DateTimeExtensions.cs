namespace NotionNotifications.Domain.Extensions;

public static class DateTimeExtensions
{
    public static DateTimeOffset GetNextDateOccurrence(
        this DateTimeOffset date, ENotificationOccurence occurence)
    {
        return occurence switch
        {
            ENotificationOccurence.Annually => date.GetNextYear(),
            ENotificationOccurence.Daily => date.GetNextDay(),
            ENotificationOccurence.Monthly => date.GetNextMonth(),
            ENotificationOccurence.Weekly => date.GetNextWeek(),
            _ => throw new InvalidOperationException(),
        };
    }

    public static DateTimeOffset GetNextDay(
        this DateTimeOffset date)
    {
        return date.AddDays(1);
    }

    public static DateTimeOffset GetNextMonth(
        this DateTimeOffset date)
    {
        return date.AddMonths(1);
    }

    public static DateTimeOffset GetNextWeek(
        this DateTimeOffset date)
    {
        return date.AddDays(7);
    }

    public static DateTimeOffset GetNextYear(
        this DateTimeOffset date)
    {
        return date.AddYears(1);
    }
}