using System.ComponentModel;

namespace NotionNotifications.Domain.Entities;

public class NotificationRoot : INotifyPropertyChanged
{

    #region Fields
    private string title = string.Empty;
    private bool alreadyNotified = false;
    private DateTimeOffset eventDate = DateTimeOffset.UtcNow;
    private DateTimeOffset lastUpdatedAt = DateTimeOffset.UtcNow;
    private DateTimeOffset createdAt = DateTimeOffset.UtcNow;
    private ENotificationOccurence occurence = ENotificationOccurence.None;
    private IEnumerable<string> categories = [];
    #endregion Fields

    public Guid IntegrationId { get; set; } = Guid.Empty;
    public int Id { get; set; }
    public string Title
    { get => title; set { title = value; OnProperyChanged(); } }
    public bool AlreadyNotified
    { get => alreadyNotified; set { alreadyNotified = value; OnProperyChanged(); } }
    public DateTimeOffset EventDate
    { get => eventDate; set { eventDate = value; OnProperyChanged(); } }
    public DateTimeOffset LastUpdatedAt
    { get => lastUpdatedAt; set { lastUpdatedAt = value; OnProperyChanged(); } }
    public DateTimeOffset CreatedAt
    { get => createdAt; set { createdAt = value; OnProperyChanged(); } }
    public ENotificationOccurence Occurence
    { get => occurence; set { occurence = value; OnProperyChanged(); } }
    public IEnumerable<string> Categories
    { get => categories; set { categories = value; OnProperyChanged(); } }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnProperyChanged(
        [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }
}