using System.Reflection;

namespace NotionNotifications.External.Libnotify;

public unsafe sealed class Notification
{
    internal static string APP_NAME = Assembly.GetExecutingAssembly().GetName().Name!;
    private IntPtr _notificationPointer;

    public Notification(
        string summary,
        string? body = null,
        string? icon = null)
    {
        Summary = summary;
        Body = body;
        Icon = icon;
        InitNotification();
    }

    public string Summary { get; set; }

    public string? Icon { get; set; }
    public string? Body { get; set; }

    public NotificationShowResult Show()
    {
        var shown = Wrapper.notify_notification_show(_notificationPointer, out IntPtr error);

        return new() { Error = error, Shown = shown };
    }

    private void InitNotification()
    {
        Wrapper.notify_init(APP_NAME);
        _notificationPointer =
            Wrapper.notify_notification_new(Summary, Body, Icon);
    }

    ~Notification()
    {
        Wrapper.notify_notification_close(_notificationPointer, out IntPtr error);
        Wrapper.notify_uninit();
    }
}