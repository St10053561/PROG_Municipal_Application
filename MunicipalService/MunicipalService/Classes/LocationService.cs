using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    public class LocationService
    {
        private const string GoogleMapsApiUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";

        public async Task<string[]> GetLocationSuggestionsAsync(string input)
        {
            using (var client = new HttpClient())
            {
                var requestUrl = $"{GoogleMapsApiUrl}?input={input}&key={Constants.GoogleMapsApiKey}";
                var response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var locationSuggestions = JsonConvert.DeserializeObject<LocationSuggestionsResponse>(responseBody);

                return locationSuggestions.Predictions.Select(p => p.Description).ToArray();
            }
        }
    }

    public class LocationSuggestionsResponse
    {
        public Prediction[] Predictions { get; set; }
    }

    public class Prediction
    {
        public string Description { get; set; }
    }
}
