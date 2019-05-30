using IdentityServer4.Configuration;
using EZNEW.Web.Security.Authentication.Cookie;
using EZNEW.Web.Security.Authentication.Unity.Server.Endpoints.Results;
using EZNEW.Web.Security.Authentication.Unity.Server.Request;
using EZNEW.Web.Security.Authentication.Unity.Server.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Web.Security.Authentication.Unity.Server
{
    /// <summary>
    /// Unity Authentication验证服务器配置
    /// </summary>
    public class UnityAuthenticationServerOption
    {
        /// <summary>
        /// 凭据验证方式
        /// </summary>
        public Func<UnityAuthenticationCredentialVerifyRequest,Task<UnityAuthenticationCredentialVerifyResult>> CredentialVerifyMethodAsync
        {
            get; set;
        }

        /// <summary>
        /// IdentityServer构造方法
        /// </summary>
        public Action<IIdentityServerBuilder> IdentityServerBuilderConfig
        {
            get;set;
        }

        /// <summary>
        // IdentityServer配置选项
        /// </summary>
        public Action<IdentityServerOptions> IdentityServerOptionsConfiguration
        {
            get;set;
        }

        /// <summary>
        /// cookie配置选项
        /// </summary>
        public Action<CustomCookieOption> CustomerCookieOptions
        {
            get;set;
        }

        /// <summary>
        /// 清除默认的输入凭据数据
        /// </summary>
        public bool ClearDefaultInboundClaim
        {
            get; set;
        } = true;

        /// <summary>
        /// 清除默认的输出凭据数据
        /// </summary>
        public bool ClearDefaultOutboundClaim
        {
            get; set;
        } = true;
    }
}
