using IdentityModel;
using IdentityServer4.Models;
using EZNEW.Web.Security.Authentication.Unity.Server.Request;
using EZNEW.Web.Security.Authentication.Unity.Server.Results;
using EZNEW.Framework.Serialize;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authentication;
using EZNEW.Framework.Net;
using EZNEW.Web.Utility;
using EZNEW.Framework.Net.Http;

namespace EZNEW.Web.Security.Authentication.Unity.Client
{
    /// <summary>
    /// 工具
    /// </summary>
    public static class UnityAuthecticationClientUtil
    {
        /// <summary>
        /// 验证登陆凭据
        /// </summary>
        /// <param name="principal">登陆凭据</param>
        /// <returns></returns>
        public static async Task<bool> VerifyCredentialAsync(ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            IConfiguration configuration = HttpContextHelper.Current.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            if (configuration == null)
            {
                throw new Exception("get IConfiguration fail");
            }
            var unityClientOptions = HttpContextHelper.Current.RequestServices.GetService<IOptionsMonitor<UnityAuthenticationClientOption>>().Get(Constants.UnityAuthenticationScheme);
            if (unityClientOptions == null)
            {
                throw new Exception("get UnityAuthenticationClientOption fail");
            }
            var openIdOption = HttpContextHelper.Current.RequestServices.GetService<IOptionsMonitor<OpenIdConnectOptions>>().Get(OpenIdConnectDefaults.AuthenticationScheme);
            if (openIdOption == null)
            {
                throw new Exception("get OpenIdConnectOptions fail");
            }
            UnityAuthenticationCredentialVerifyRequest request = new UnityAuthenticationCredentialVerifyRequest()
            {
                Client = new IdentityServer4.Models.Client()
                {
                    ClientId = openIdOption.ClientId,
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret(openIdOption.ClientSecret.Sha256())
                    }
                },
                Claims = principal.Claims.ToDictionary(c => c.Type, c => c.Value)
            };
            string url = unityClientOptions.CredentialVerifyUrl;
            if (string.IsNullOrWhiteSpace(url))
            {
                url = openIdOption.Authority + "/" + Constants.RoutePaths.CredentialVerify;
            }
            try
            {
                var result = await HttpUtil.HttpPostJsonAsync(url, request).ConfigureAwait(false);
                var stringValue = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                UnityAuthenticationCredentialVerifyResult verifyResult = JsonSerialize.JsonToObject<UnityAuthenticationCredentialVerifyResult>(stringValue);
                var loginSuccess = verifyResult?.VerifySuccess ?? false;
                return loginSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 验证Cookie登陆凭据
        /// </summary>
        /// <param name="principalContext"></param>
        /// <returns></returns>
        public static async Task<bool> VerifyCredentialAsync(CookieValidatePrincipalContext principalContext)
        {
            if (principalContext == null)
            {
                return false;
            }
            return await VerifyCredentialAsync(principalContext.Principal, principalContext.Properties).ConfigureAwait(false);
        }
    }
}
