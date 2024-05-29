using Exception_Handling.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExceptionHandling.Controllers
{
    /// <summary>
    /// Controller to demonstrate exception handling in an HTTP context.
    /// </summary>
    public class CLExceptionController : ApiController
    {
       
        #region HandleHttpRequest
        /// <summary>
        /// Handles an HTTP request, simulating request/response with exception handling.
        /// </summary>

        [HttpGet]
        [Route("api/demo/handleHttpRequest")]
        public IHttpActionResult HandleHttpRequest()
        {
            try
            {
                // Simulating an HTTP request
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync("https://example.com").Result;
                    response.StatusCode = HttpStatusCode.Forbidden;
                    response.EnsureSuccessStatusCode(); // This may throw HttpRequestException

                    // Process the response
                    if (response.IsSuccessStatusCode)
                    {
                        // Successful response handling logic
                        Logger.LogInformation("HTTP request handled successfully");
                        return Ok("Request handled successfully");
                    }
                    else
                    {
                        // Handle non-success status codes
                        Logger.LogWarning($"Non-success status code: {response.StatusCode}");
                        return Content(response.StatusCode, "An error occurred during the request.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception using our Logger
                Logger.LogError(ex, "Error in handling HTTP request");

                // Handle the exception and provide a user-friendly response
                return Content(HttpStatusCode.InternalServerError, "An error occurred during the request.");
            }
        }
        #endregion

        #region HandleHttpResponse
        /// <summary>
        /// Handles an HTTP response, simulating response handling with exception handling.
        /// </summary>
        [HttpGet]
        [Route("api/demo/handleHttpResponse")]
        public IHttpActionResult HandleHttpResponse()
        {
            try
            {

                // For demonstration purposes, let's throw a custom HttpResponseException
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (HttpResponseException ex)
            {
                // Log the exception using our Logger
                Logger.LogError(ex, "Error in handling HTTP response");

                // Handle the exception and provide a user-friendly response
                return Content(HttpStatusCode.InternalServerError, "An error occurred while processing the response.");
            }
        }
        #endregion

        #region HandleHttpRequestWithFilter
        /// <summary>
        /// Handles an HTTP request with a filter for specific exceptions, demonstrating retry logic.
        /// </summary>
        [HttpGet]
        [Route("api/demo/handleHttpRequestWithFilter")]
        public IHttpActionResult HandleHttpRequestWithFilter()
        {
            try
            {
                // Simulating an HTTP request
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync("https://example.com").Result;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.EnsureSuccessStatusCode(); // This may throw HttpRequestException

                    // Process the response
                    if (response.IsSuccessStatusCode)
                    {
                        // Successful response handling logic
                        Logger.LogInformation("HTTP request handled successfully");
                        return Ok("Request handled successfully");
                    }
                    else
                    {
                        // Handle non-success status codes
                        Logger.LogWarning($"Non-success status code: {response.StatusCode}");
                        return Content(response.StatusCode, "An error occurred during the request.");
                    }
                }
            }
            catch (HttpRequestException ex) when (IsNetworkIssue(ex))
            {
                // Log the exception using different log level
                Logger.LogWarning("Retrying HTTP request due to network issue");
                Logger.LogDebug($"Details: {ex}");

                // Attempt to retry the request
                RetryHttpRequest();

                return Ok("Request retried successfully");
            }
            catch (HttpRequestException ex)
            {
                // Log other HTTP request exceptions
                Logger.LogError(ex, "Error in handling HTTP request");

                // Handle the exception and provide a user-friendly response
                return Content(HttpStatusCode.InternalServerError, "An error occurred during the request.");
            }
        }

        /// <summary>
        /// Determines if the provided HttpRequestException is due to a network issue.
        /// </summary>
        private bool IsNetworkIssue(HttpRequestException ex)
        {
            // Placeholder method for determining if the exception is due to a network issue
            // Implement your logic here based on the actual exception details
            return true;
        }

        /// <summary>
        /// Retries an HTTP request with a maximum number of attempts.
        /// </summary>
        private void RetryHttpRequest()
        {
            const int maxRetryAttempts = 3;
            int currentAttempt = 0;

            while (currentAttempt < maxRetryAttempts)
            {
                try
                {
                    // Simulate retry logic
                    Logger.LogInformation($"Retry attempt {currentAttempt + 1}");

                    // Simulating an HTTP request
                    using (HttpClient client = new HttpClient())
                    {
                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri("https://quotes85.p.rapidapi.com/keyword?word=success"),
                            Headers ={
                                    { "X-RapidAPI-Key", "fb9a25b181msh5c88c6d0aa36a4bp128766jsn26c140c6f064" },
                                    { "X-RapidAPI-Host", "quotes85.p.rapidapi.com" },},
                        };
                        HttpResponseMessage response = client.GetAsync("request").Result;
                        response.EnsureSuccessStatusCode(); // This may throw HttpRequestException

                        // Process the response
                        // ...

                        Logger.LogInformation("HTTP request retried successfully");
                        return;
                    }
                }
                catch (HttpRequestException ex) when (IsNetworkIssue(ex))
                {
                    // Log the exception using different log level
                    Logger.LogWarning("Retrying HTTP request due to network issue");
                    Logger.LogDebug($"Details: {ex}");

                    // Increment the retry attempt
                    currentAttempt++;
                }
                catch (HttpRequestException ex)
                {
                    // Log other HTTP request exceptions
                    Logger.LogError(ex, "Error in retrying HTTP request");

                    // Handle the exception and provide a user-friendly response
                    Logger.LogWarning("Retry failed. Maximum attempts reached.");
                    return;
                }
            }

            Logger.LogWarning("Retry failed. Maximum attempts reached.");
        }
        #endregion
    }
}
