using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Filter.Filters
{
    /// <summary>
    /// Action filter to add a joke to the response headers.
    /// </summary>
    public class JokeActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        #region Private Fields

        private readonly ILogger<JokeActionFilter> _logger;
        private readonly HttpClient _httpClient;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the order in which the filter is applied.
        /// </summary>
        public int Order { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="JokeActionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="order">The order in which the filter is applied.</param>
        public JokeActionFilter(ILogger<JokeActionFilter> logger, IHttpClientFactory httpClientFactory, int order = 0)
        {
            Order = order;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes asynchronously before and after the action method is invoked.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        /// <param name="next">The delegate representing the remaining action execution pipeline.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                // Fetch a joke from the JokeAPI
                HttpResponseMessage response = await _httpClient.GetAsync("https://v2.jokeapi.dev/joke/Programming?blacklistFlags=nsfw,religious,racist,sexist&type=single&idRange=0-200");

                if (response.IsSuccessStatusCode)
                {
                    string jokeContent = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response
                    dynamic jokeJson = JsonConvert.DeserializeObject(jokeContent);
                    string joke = jokeJson.joke;

                    // Add the joke as a response header
                    context.HttpContext.Response.Headers.Add("X-Joke", joke);

                    _logger.LogInformation($"Joke added to the response headers: {joke}");
                }
                else
                {
                    _logger.LogError($"Failed to fetch joke from the JokeAPI. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching the joke: {ex.Message}");
            }

            // Continue to the action method
            _ = await next();
        }

        #endregion
    }
}
