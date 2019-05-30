using EZNEW.Web.Security.Authentication.Unity.Server.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class UnityAuthenticationApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseUnityAuthentication(this IApplicationBuilder app)
        {
            app.UseMiddleware<UnityAuthenticationMiddleware>();
            app.UseIdentityServer();
            return app;
        }
    }
}
