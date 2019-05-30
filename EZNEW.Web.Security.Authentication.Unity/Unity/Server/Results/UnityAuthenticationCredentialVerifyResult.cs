using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Web.Security.Authentication.Unity.Server.Results
{
    public class UnityAuthenticationCredentialVerifyResult
    {
        /// <summary>
        /// 验证通过
        /// </summary>
        public bool VerifySuccess
        {
            get;set;
        }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message
        {
            get;set;
        }
    }
}
