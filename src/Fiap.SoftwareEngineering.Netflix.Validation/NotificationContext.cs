using Fiap.SoftwareEngineering.Netflix.Validation.Abstractions;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Fiap.SoftwareEngineering.Netflix.Validation
{
    public class NotificationContext : INotificationContext
    {
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();
        private readonly List<Notification> _notifications;

        public NotificationContext() => _notifications = new List<Notification>();

        public virtual void AddNotification(string key, string message) => _notifications.Add(new Notification(key, message));

        public virtual void AddNotification(Notification notification) => _notifications.Add(notification);

        public virtual void AddNotifications(IReadOnlyCollection<Notification> notifications) => _notifications.AddRange(notifications);

        public virtual void AddNotifications(IList<Notification> notifications) => _notifications.AddRange(notifications);

        public virtual void AddNotifications(ICollection<Notification> notifications) => _notifications.AddRange(notifications);

        public virtual void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                AddNotification(error.ErrorCode, error.ErrorMessage);
        }
    }
}
