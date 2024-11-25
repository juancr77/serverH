using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TravelApi.Models;

namespace TravelApi.Services
{
    public class OsrmService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public OsrmService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["OSRM:BaseUrl"];
        }

        public async Task<RouteResponse> GetRouteAsync(string start, string end)
        {
            var url = $"{_baseUrl}/route/v1/driving/{start};{end}?overview=full&geometries=geojson";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            return new RouteResponse
            {
                Distance = json["routes"]?[0]?["distance"]?.Value<double>() ?? 0,
                Duration = json["routes"]?[0]?["duration"]?.Value<double>() ?? 0,
                Coordinates = json["routes"]?[0]?["geometry"]?["coordinates"]?.ToObject<List<List<double>>>() ?? new List<List<double>>()
            };
        }
    }
}
