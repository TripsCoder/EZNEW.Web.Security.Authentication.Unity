using IdentityModel;
using EZNEW.Framework.IoC;
using EZNEW.Web.Config.App;
using EZNEW.Web.Security.Authentication.Cookie;
using EZNEW.Web.Security.Authentication.Session;
using EZNEW.Web.Security.Authentication.Unity;
using EZNEW.Web.Security.Authentication.Unity.Client;
using EZNEW.Web.Security.Authentication.Unity.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnityAuthenticationClientServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddUnityClientAuthentication(this IServiceCollection services, Action<UnityAuthenticationClientOption> optionConfiguration = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            var clientOption = new UnityAuthenticationClientOption();

            #region 默认授权配置

            var unityAuthConfig = ContainerManager.Resolve<IOptions<UnityAuthClientConfig>>()?.Value;
            if (unityAuthConfig != null)
            {
                clientOption.CredentialVerifyUrl = unityAuthConfig.CredentialVerifyUrl;
                clientOption.ClearDefaultInboundClaim = unityAuthConfig.ClearDefaultInboundClaim;
                clientOption.ClearDefaultOutboundClaim = unityAuthConfig.ClearDefaultOutboundClaim;
            }
            var applicationConfig = ContainerManager.Resolve<IOptions<ApplicationConfig>>()?.Value;
            optionConfiguration?.Invoke(clientOption);
            var builder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                clientOption?.AuthenticationConfiguration?.Invoke(options);
            });
            if (clientOption.ClearDefaultInboundClaim)
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            }
            if (clientOption.ClearDefaultOutboundClaim)
            {
                JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            }

            #endregion

            #region Cookie认证配置

            Action<CustomCookieOption> cookieDefaultConfiguration = options =>
            {
                options.ForceValidatePrincipal = true;
                options.ValidatePrincipalAsync = UnityAuthecticationClientUtil.VerifyCredentialAsync;
            };
            if (clientOption.CookieConfiguration != null)
            {
                cookieDefaultConfiguration += clientOption.CookieConfiguration;
            }
            builder.AddCookieAuthentication(cookieDefaultConfiguration);

            #endregion

            #region OpenIdConnect Authentication

            var sessionConfig = SessionConfig.GetSessionConfig();
            Action<OpenIdConnectOptions> defaultOpenIdConnectConfiguration = openIdOptions =>
            {
                openIdOptions.ClientId = applicationConfig?.Current?.AppCode ?? string.Empty;
                openIdOptions.ClientSecret = applicationConfig?.Current?.AppSecret ?? string.Empty;
                openIdOptions.Authority = unityAuthConfig?.UnityServer ?? string.Empty;
                openIdOptions.RequireHttpsMetadata = false;
                openIdOptions.ResponseType = OpenIdConnectResponseType.CodeIdTokenToken;
                openIdOptions.SaveTokens = true;
                openIdOptions.GetClaimsFromUserInfoEndpoint = true;
                openIdOptions.TokenValidationParameters.NameClaimType = JwtClaimTypes.Name;
                openIdOptions.Scope.Add(sessionConfig.SessionClaimName);
                openIdOptions.ClaimActions.MapUniqueJsonKey(sessionConfig.SessionClaimName, sessionConfig.SessionClaimName);
            };
            if (clientOption.OpenIdConnectConfiguration != null)
            {
                defaultOpenIdConnectConfiguration += clientOption.OpenIdConnectConfiguration;
            }
            builder.AddOpenIdConnect(defaultOpenIdConnectConfiguration);

            #endregion

            if (optionConfiguration != null)
            {
                builder.Services.Configure(Constants.UnityAuthenticationScheme, optionConfiguration);
            }
            return builder;
        }
    }
}
