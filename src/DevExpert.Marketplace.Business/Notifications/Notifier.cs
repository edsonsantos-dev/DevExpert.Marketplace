using DevExpert.Marketplace.Business.Interfaces.Notifications;

namespace DevExpert.Marketplace.Business.Notifications;

public class Notifier : INotifier
{
    private List<Notification> _notifications;

    public Notifier()
    {
        _notifications = [];
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }
    
    public bool HaveNotification()
    {
        return _notifications.Count != 0;
    }

    public List<Notification> GetNotifications()
    {
        return _notifications;
    }
}