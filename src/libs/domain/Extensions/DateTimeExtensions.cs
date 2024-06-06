namespace NotionNotifications.Domain.Extensions;

public static class DateTimeExtensions
{
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