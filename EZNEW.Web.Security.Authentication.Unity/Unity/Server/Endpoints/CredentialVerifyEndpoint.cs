using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Web.Security.Authentication.Unity.Server.Endpoints.Results;
using EZNEW.Web.Security.Authentication.Unity.Server.Request;
using EZNEW.Web.Security.Authentication.Unity.Server.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using EZNEW.Framework.Serialize;

namespace EZNEW.Web.Security.Authentication.Unity.Server.Endpoints
{
    public class CredentialVerifyEndpoint : UnityAuthenticationEndpointBase
    {
        public CredentialVerifyEndpoint(ILogger<CredentialVerifyEndpoint> logger, IOptions<UnityAuthenticationServerOption> unityAuthenticationOption) : base(logger, unityAuthenticationOption)
        {

        }

        public override async Task<IUnityAuthenticationEndpointResult> ProcessAsync(HttpContext context)
        {
            Logger.LogDebug("Start Credential Verify");
            if (!HttpMethods.IsPost(context.Request.Method))
            {
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            var credentialVerifyRequest = BuildCredentialVerifyRequest(context);
            if (credentialVerifyRequest == null)
            {
                Logger.LogDebug("Build CredentialVeriryRequest object error");
                return new StatusCodeResult(HttpStatusCode.InternalServerError);
            }
            if (UnityAuthenticationOption.CredentialVerifyMethodAsync == null)
            {
                Logger.LogError("haven't configured any CredentialVerifyMethod value");
                return new StatusCodeResult(HttpStatusCode.InternalServerError);
            }
            var result = await UnityAuthenticationOption.CredentialVerifyMethodAsync(credentialVerifyRequest).ConfigureAwait(false);
            return BuildCredentialVerifyEndpointResult(result);
        }

        UnityAuthenticationCredentialVerifyRequest BuildCredentialVerifyRequest(HttpContext context)
        {
            string requestValue = string.Empty;
            StreamReader reader = new StreamReader(context.Request.Body);
            requestValue = reader.ReadToEnd();
            if (string.IsNullOrWhiteSpace(requestValue))
            {
                return null;
            }
            try
            {
                var request = JsonSerialize.JsonToObject<UnityAuthenticationCredentialVerifyRequest>(requestValue);
                return request;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return null;
            }
        }

        CredentialVerifyEndpointResult BuildCredentialVerifyEndpointResult(UnityAuthenticationCredentialVerifyResult verifyResult)
        {
            return new CredentialVerifyEndpointResult()
            {
                VerifyResult = verifyResult
            };
        }
    }
}
