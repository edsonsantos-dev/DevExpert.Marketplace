namespace DevExpert.Marketplace.Core.Notifications;

public interface INotifier
{
    bool HaveNotification();
    List<Notification> GetNotifications();
    void AddNotification(Notification notification);
}