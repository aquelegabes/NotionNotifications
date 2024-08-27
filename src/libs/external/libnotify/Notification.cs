using System.Reflection;

namespace NotionNotifications.External.Libnotify;

public unsafe sealed class Notification {
    internal static string APP_NAME = Assembly.GetExecutingAssembly().GetName().Name!;
    private IntPtr _notificationPointer;

    public Notification(
        string summary,
        string? body = null,
        string? icon = null) 
    {
        this.Summary = summary;
        this.Body = body;
        this.Icon = icon;
        InitNotification();
    }

    public string Summary { get; set; }

    /// <summary>
    /// Check https://specifications.freedesktop.org/icon-naming-spec/latest/ for available icons.
    /// </summary> 
    public string? Icon { get; set; }
    public string? Body { get; set; }

    public NotificationShowResult Show() { 
        var shown = LibnotifyWrapper.notify_notification_show(_notificationPointer, out IntPtr error);

        return new() { Error = error, Shown = shown };
    }

    private void InitNotification() {
        LibnotifyWrapper.notify_init(APP_NAME);
        _notificationPointer = 
            LibnotifyWrapper.notify_notification_new(this.Summary, this.Body, this.Icon);
    }

    ~Notification() {
        LibnotifyWrapper.notify_notification_close(_notificationPointer, out IntPtr error);
        LibnotifyWrapper.notify_uninit();
    }
}