namespace NotionNotifications.External.Libnotify;

/// <summary>
/// Check https://specifications.freedesktop.org/icon-naming-spec/latest/ for available icons.
/// </summary>
public static class Icons
{
    public static class Emblem
    {
        public const string IMPORTANT = "emblem-important";
        public const string FAVORITE = "emblem-favorite";
    }

    public static class StandardDevice
    {
        public const string PHONE = "phone";
        public const string CAMERA_PHOTO = "camera-photo";
        public const string CAMEA_WEB = "camera-web";
    }

    public static class Status
    {
        public const string APPOINTMENT_SOON = "appointment-soon";
    }
}
