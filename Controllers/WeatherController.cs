using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

public class WeatherController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WeatherController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetWeather(string location)
    {
        string apiKey = "0d5dc38d0176cef1cc4690413d0a2778";
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={apiKey}";

        using (HttpClient client = _httpClientFactory.CreateClient())
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                // Parse the weather data and return it as JSON
                return Json(data);
            }
            else
            {
                return BadRequest($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
