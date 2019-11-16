﻿using System;
using System.Net;
using System.Threading.Tasks;
using Fiap.SoftwareEngineering.Netflix.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace Fiap.SoftwareEngineering.Netflix.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate Next;
        static readonly ILogger Log = Serilog.Log.ForContext<ExceptionMiddleware>();

        public ExceptionMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = ContentTypes.ApplicationJson;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                Log.Error(ex.Message, ex);

                await context
                    .Response
                    .WriteAsync(JsonConvert
                        .SerializeObject(ex,
                            new JsonSerializerSettings {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            }));
            }
        }
    }
}
