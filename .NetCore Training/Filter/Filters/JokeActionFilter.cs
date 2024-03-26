using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public class JokeActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<JokeActionFilter> _logger;
        private readonly HttpClient _httpClient;

        public JokeActionFilter(ILogger<JokeActionFilter> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                // Fetch a joke from the JokeAPI
                var response = await _httpClient.GetAsync("https://v2.jokeapi.dev/joke/Programming?blacklistFlags=nsfw,religious,racist,sexist&type=single&idRange=0-200");

                if (response.IsSuccessStatusCode)
                {
                    var jokeContent = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response
                    dynamic jokeJson = Newtonsoft.Json.JsonConvert.DeserializeObject(jokeContent);
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
            await next();
        }
    }
}
