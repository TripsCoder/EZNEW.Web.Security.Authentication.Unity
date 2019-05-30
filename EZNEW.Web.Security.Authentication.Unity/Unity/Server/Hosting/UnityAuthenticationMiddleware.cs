using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Web.Security.Authentication.Unity.Server.Hosting
{
    public class UnityAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public UnityAuthenticationMiddleware(RequestDelegate next, ILogger<UnityAuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = EndpointProvider.FindEndpoint(context);
            if (endpoint != null)
            {
                _logger.LogInformation("Invoking Unity Authentication endpoint: {endpointType} for {url}", endpoint.GetType().FullName, context.Request.Path.ToString());
                var result = await endpoint.ProcessAsync(context);
                if (result != null)
                {
                    _logger.LogTrace("Invoking result: {type}", result.GetType().FullName);
                    await result.ExecuteAsync(context);
                }
                return;
            }
            await _next(context);
        }
    }
}
