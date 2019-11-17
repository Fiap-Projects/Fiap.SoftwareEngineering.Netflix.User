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
        private readonly RequestDelegate _next;
        private readonly INotificationContext _notificationContext;

        public NotificationMiddleware(RequestDelegate next, INotificationContext notificationContext)
        {
            _next = next;
            _notificationContext = notificationContext;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_notificationContext.HasNotifications)
            {
                context.Response.ContentType = ContentTypes.ApplicationJson;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);
                await context.Response.WriteAsync(notifications);
                return;
            }

            await _next(context);
        }
    }
}
