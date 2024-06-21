using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace ATM_Simulation_Demo.Others.Auth
{
    /// <summary>
    /// Custom authentication filter for handling authentication in Web API requests.
    /// </summary>
    public class CustomAuthenticationFilter : AuthorizeAttribute, IAuthenticationFilter
    {
        public override bool AllowMultiple => false;

        /// <summary>
        /// Authenticates the incoming HTTP request.
        /// </summary>
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string authParameter = string.Empty;
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Authorization header", request);
                return;
            }

            if (authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid Authorization Schema", request);
                return;
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Token not Available", request);
                return;
            }

            // Validate the token using TokenManager.GetPrincipal
            var principal = TokenManager.GetPrincipal(authorization.Parameter);

            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid Token", request);
                return;
            }

            context.Principal = principal;
        }

        /// <summary>
        /// Challenges the client to authenticate.
        /// </summary>
        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);

            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Customize the challenge header based on your authentication scheme
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Bearer", "realm=localhost"));
            }

            context.Result = new ResponseMessageResult(result);
        }
    }

    /// <summary>
    /// Represents a response for authentication failure.
    /// </summary>
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string ReasonPhrase;
        public HttpRequestMessage Request { get; set; }

        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage httpRequestMessage)
        {
            ReasonPhrase = reasonPhrase;
            Request = httpRequestMessage;
        }

        /// <summary>
        /// Executes the authentication failure result.
        /// </summary>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        /// <summary>
        /// Executes the authentication failure result.
        /// </summary>
        public HttpResponseMessage Execute()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            responseMessage.RequestMessage = Request;
            responseMessage.ReasonPhrase = ReasonPhrase;
            return responseMessage;
        }
    }
}
