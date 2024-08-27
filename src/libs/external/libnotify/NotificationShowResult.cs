namespace NotionNotifications.External.Libnotify;

public unsafe sealed class NotificationShowResult {
    public bool Shown { get;set; } = false;
    public IntPtr? Error { get; set; } = null;
}