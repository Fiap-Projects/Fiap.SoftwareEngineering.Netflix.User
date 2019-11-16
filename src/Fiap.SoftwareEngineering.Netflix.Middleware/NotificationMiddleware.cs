using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.Http;
using Fiap.SoftwareEngineering.Netflix.Validation.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fiap.SoftwareEngineering.Netflix.Middleware
{
    public class NotificationMiddleware
    {
        private readonly RequestDelegate Next;
        private readonly INotificationContext NotificationContext;

        public NotificationMiddleware(RequestDelegate next, INotificationContext notificationContext)
        {
            Next = next;
            NotificationContext = notificationContext;
        }

        public async Task Invoke(HttpContext context)
        {
            if (NotificationContext.HasNotifications)
            {
                context.Response.ContentType = ContentTypes.ApplicationJson;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var notifications = JsonConvert.SerializeObject(NotificationContext.Notifications);
                await context.Response.WriteAsync(notifications);
                return;
            }

            await Next(context);
        }
    }
}
