using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace ATM_Simulation_Demo
{
    /// <summary>
    /// Custom authentication filter for authenticating requests using bearer tokens.
    /// </summary>
    public class CustomAuthenticationFilter : AuthorizeAttribute, IAuthenticationFilter
    {
        /// <summary>
        /// Indicates whether multiple instances of the filter can be allowed.
        /// </summary>
        public override bool AllowMultiple => false;

        /// <summary>
        /// Authenticates the request asynchronously.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string authParameter = string.Empty;
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                // If Authorization header is missing, set error result.
                context.ErrorResult = new AuthenticationFailureResult("Missing Authorization header", request);
                return;
            }

            if (authorization.Scheme != "Bearer")
            {
                // If the authorization scheme is not 'Bearer', set error result.
                context.ErrorResult = new AuthenticationFailureResult("Invalid Authorization Schema", request);
                return;
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                // If token is not available, set error result.
                context.ErrorResult = new AuthenticationFailureResult("Token not Available", request);
                return;
            }

            // Validate the token using TokenManager.GetPrincipal.
            var principal = TokenManager.GetPrincipal(authorization.Parameter);

            if (principal == null)
            {
                // If the token is invalid, set error result.
                context.ErrorResult = new AuthenticationFailureResult("Invalid Token", request);
                return;
            }

            // Set the principal for the current context.
            context.Principal = principal;
        }

        /// <summary>
        /// Handles authentication challenges asynchronously.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            // Execute the result of the authentication challenge.
            var result = await context.Result.ExecuteAsync(cancellationToken);

            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Add a custom challenge header for the 'Bearer' authentication scheme.
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Bearer", "realm=localhost"));
            }

            // Set the response message result.
            context.Result = new ResponseMessageResult(result);
        }

    }

    /// <summary>
    /// Represents a result for authentication failure.
    /// </summary>
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string ReasonPhrase;
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        /// Initializes a new instance of the AuthenticationFailureResult class.
        /// </summary>
        /// <param name="reasonPhrase"></param>
        /// <param name="httpRequestMessage"></param>
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage httpRequestMessage)
        {
            ReasonPhrase = reasonPhrase;
            Request = httpRequestMessage;
        }

        /// <summary>
        /// Executes the asynchronous HTTP action result.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        /// <summary>
        /// Executes the HTTP action result.
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
