using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Web.Security.Authentication.Unity.Config
{
    /// <summary>
    /// Unity Authentication Client Config
    /// </summary>
    public class UnityAuthClientConfig
    {
        /// <summary>
        /// 凭据验证地址
        /// </summary>
        public string CredentialVerifyUrl
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
        } = false;

        /// <summary>
        /// 统一服务地址
        /// </summary>
        public string UnityServer
        {
            get;set;
        }
    }
}
