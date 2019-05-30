using EZNEW.Web.Security.Authentication.Cookie;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Web.Security.Authentication.Unity.Client
{
    public class UnityAuthenticationClientOption
    {
        /// <summary>
        /// 凭据验证地址
        /// </summary>
        public string CredentialVerifyUrl
        {
            get; set;
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
        } = false;

        /// <summary>
        /// OpenId 验证配置
        /// </summary>
        public Action<OpenIdConnectOptions> OpenIdConnectConfiguration
        {
            get; set;
        }

        /// <summary>
        /// Cookie配置
        /// </summary>
        public Action<CustomCookieOption> CookieConfiguration
        {
            get; set;
        }

        /// <summary>
        /// 授权配置
        /// </summary>
        public Action<AuthenticationOptions> AuthenticationConfiguration
        {
            get;set;
        }
    }
}
