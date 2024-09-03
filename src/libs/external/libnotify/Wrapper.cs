using System.Runtime.InteropServices;

namespace NotionNotifications.External.Libnotify;

internal partial class Wrapper
{
    [LibraryImport(
        libraryName: "libnotify.so.4")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    internal static partial string notify_get_app_name();

    [LibraryImport(
        libraryName: "libnotify.so.4")]
    internal static partial void notify_uninit();

    [LibraryImport(
        libraryName: "libnotify.so.4")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool notify_init(
        [MarshalAs(UnmanagedType.LPStr)] string appName);

    [LibraryImport(
        libraryName: "libnotify.so.4",
        StringMarshalling = StringMarshalling.Utf8)]
    internal static partial IntPtr notify_notification_new(
        [MarshalAs(UnmanagedType.LPStr)] string summary,
        [MarshalAs(UnmanagedType.LPStr)] string? body,
        [MarshalAs(UnmanagedType.LPStr)] string? icon);

    [LibraryImport(libraryName: "libnotify.so.4")]
    internal static partial IntPtr notify_notification_close(
        IntPtr notification,
        out IntPtr error);

    [LibraryImport(
        libraryName: "libnotify.so.4",
        StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool notify_notification_show(IntPtr notification, out IntPtr error);
}
