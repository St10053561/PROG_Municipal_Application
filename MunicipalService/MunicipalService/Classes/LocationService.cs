using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    // This class provides location-related services, like getting location suggestions from Google Maps API.
    public class LocationService
    {
        // URL for the Google Maps API endpoint
        private const string GoogleMapsApiUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";

        // This method fetches location suggestions based on the input string.
        public async Task<string[]> GetLocationSuggestionsAsync(string input)
        {
            using (var client = new HttpClient())
            {
                // Construct the request URL with the input and API key
                var requestUrl = $"{GoogleMapsApiUrl}?input={input}&key={Constants.GoogleMapsApiKey}";

                // Send the GET request to the API
                var response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to a LocationSuggestionsResponse object
                var locationSuggestions = JsonConvert.DeserializeObject<LocationSuggestionsResponse>(responseBody);

                // Extract and return the descriptions of the location suggestions
                return locationSuggestions.Predictions.Select(p => p.Description).ToArray();
            }
        }
    }

    // This class represents the response from the Google Maps API for location suggestions.
    public class LocationSuggestionsResponse
    {
        // Array of predictions provided by the API
        public Prediction[] Predictions { get; set; }
    }

    // This class represents a single prediction from the Google Maps API.
    public class Prediction
    {
        // Description of the predicted location
        public string Description { get; set; }
    }
}
