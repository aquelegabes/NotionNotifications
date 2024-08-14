using System.ComponentModel;

namespace NotionNotifications.Domain;

public enum ENotificationOccurence
{
    [Description("Único")]
    None = 0,
    [Description("Diariamente")]
    Daily,
    [Description("Semanalmente")]
    Weekly,
    [Description("Mensalmente")]
    Monthly,
    [Description("Anualmente")]
    Annually
}
