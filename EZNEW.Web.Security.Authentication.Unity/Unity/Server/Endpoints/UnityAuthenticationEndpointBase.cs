using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Web.Security.Authentication.Unity.Server.Endpoints.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EZNEW.Web.Security.Authentication.Unity.Server.Endpoints
{
    public abstract class UnityAuthenticationEndpointBase : IUnityAuthenticationEndpointHandler
    {
        public UnityAuthenticationEndpointBase(ILogger<UnityAuthenticationEndpointBase> logger,IOptions<UnityAuthenticationServerOption> unityAuthenticationOption)
        {
            Logger = logger;
            UnityAuthenticationOption = unityAuthenticationOption.Value;
        }

        public abstract Task<IUnityAuthenticationEndpointResult> ProcessAsync(HttpContext context);

        protected ILogger Logger { get; private set; }

        protected UnityAuthenticationServerOption UnityAuthenticationOption
        {
            get;set;
        }
    }
}
