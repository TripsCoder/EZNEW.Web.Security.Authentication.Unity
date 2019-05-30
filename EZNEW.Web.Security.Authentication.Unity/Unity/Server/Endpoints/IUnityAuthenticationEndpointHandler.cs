using EZNEW.Web.Security.Authentication.Unity.Server.Endpoints.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Web.Security.Authentication.Unity.Server.Endpoints
{
    public interface IUnityAuthenticationEndpointHandler
    {
        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns></returns>
        Task<IUnityAuthenticationEndpointResult> ProcessAsync(HttpContext context);
    }
}
