using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace EZNEW.Web.Security.Authentication.Unity.Server.Request
{
    /// <summary>
    /// 凭据验证请求数据
    /// </summary>
    public class UnityAuthenticationCredentialVerifyRequest
    {
        /// <summary>
        /// 客户端
        /// </summary>
        public IdentityServer4.Models.Client Client
        {
            get; set;
        }

        /// <summary>
        /// 验证凭据
        /// </summary>
        public Dictionary<string, string> Claims
        {
            get; set;
        }
    }
}
