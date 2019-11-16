using System.Collections.Generic;
using FluentValidation.Results;

namespace Fiap.SoftwareEngineering.Netflix.Validation.Abstractions
{
    public interface INotificationContext
    {
        bool HasNotifications { get; }
        IReadOnlyCollection<Notification> Notifications { get; }

        void AddNotification(string key, string message);
        void AddNotification(Notification notification);
        void AddNotifications(IReadOnlyCollection<Notification> notifications);
        void AddNotifications(IList<Notification> notifications);
        void AddNotifications(ICollection<Notification> notifications);
        void AddNotifications(ValidationResult validationResult);
    }
}
