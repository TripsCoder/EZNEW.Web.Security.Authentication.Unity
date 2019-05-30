using EZNEW.Web.Security.Authentication.Cookie;
using EZNEW.Web.Security.Authentication.Unity.Server;
using EZNEW.Web.Security.Authentication.Unity.Server.Hosting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnityAuthenticationServerServiceCollectionExtensions
    {
        public static void AddUnityServerAuthentication(this IServiceCollection services, Action<UnityAuthenticationServerOption> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }
            services.Configure(configureOptions);
            services.AddDefaultsEndpoints();//默认处理节点
            var authenticationOption = new UnityAuthenticationServerOption();
            configureOptions?.Invoke(authenticationOption);
            if (authenticationOption.ClearDefaultInboundClaim)
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            }
            if (authenticationOption.ClearDefaultOutboundClaim)
            {
                JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            }

            #region identityserver

            IIdentityServerBuilder identityServerBuilder = null;
            if (authenticationOption.IdentityServerOptionsConfiguration != null)
            {
                identityServerBuilder = services.AddIdentityServer(authenticationOption.IdentityServerOptionsConfiguration);
            }
            else
            {
                identityServerBuilder = services.AddIdentityServer();
            }
            authenticationOption.IdentityServerBuilderConfig?.Invoke(identityServerBuilder); 

            #endregion

            //cookie
            services.AddAuthentication().AddCookieAuthentication(authenticationOption.CustomerCookieOptions);
        }
    }
}
