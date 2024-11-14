using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;

namespace MunicipalService.Classes
{
    // Static utility class for handling weather data and time updates
    public static class WeatherAndTimeUtility
    {
        // API key for accessing the OpenWeather API
        private const string ApiKey = "73e64f4603ce99b1b81a64d0f7363b2d";
        // Base URL for the weather API
        private const string WeatherUrl = "https://api.openweathermap.org/data/2.5/weather";

        // Method to initialize a timer that updates the current time every second
        public static void InitializeTimer(TextBlock timeTextBlock)
        {
            DispatcherTimer timer = new DispatcherTimer(); // Create a new DispatcherTimer
            timer.Interval = TimeSpan.FromSeconds(1); // Set the timer interval to 1 second
            timer.Tick += (sender, e) =>
            {
                // Update the TextBlock with the current date and time
                timeTextBlock.Text = DateTime.Now.ToString("ddd, MMMM dd yyyy, hh:mm tt");
            };
            timer.Start(); // Start the timer
        }

        // Method to fetch weather data asynchronously and update the provided TextBlock
        public static async Task FetchWeatherData(TextBlock temperatureTextBlock)
        {
            using (HttpClient client = new HttpClient()) // Create a new HttpClient instance
            {
                // Construct the URL for the weather API request
                string url = $"{WeatherUrl}?lat=-33.918861&lon=18.423300&appid={ApiKey}&units=metric";
                try
                {
                    // Send a GET request to the weather API
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode) // Check if the response is successful
                    {
                        // Read the response content as a string
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        // Parse the JSON response
                        JObject jsonObject = JObject.Parse(jsonResponse);
                        // Extract the temperature value from the JSON response
                        double temperature = jsonObject["main"]["temp"].Value<double>();
                        // Extract the weather description from the JSON response
                        string weatherText = jsonObject["weather"][0]["description"].ToString();
                        // Update the TextBlock with the temperature and weather description
                        temperatureTextBlock.Text = $"{Math.Round(temperature, 1)} °C, {weatherText}";
                    }
                    else // If the response is not successful
                    {
                        // Read the error response content as a string
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) // Check if the status code is Unauthorized
                        {
                            // Show a message box indicating that the API limit has been exceeded
                            MessageBox.Show("Error fetching weather conditions: Unauthorized. The allowed number of requests has been exceeded. Please check your API key usage or try again later.", "API Limit Exceeded", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else // For other status codes
                        {
                            // Show a message box with the error reason and response
                            MessageBox.Show($"Error fetching weather conditions: {response.ReasonPhrase}\n{errorResponse}");
                        }
                    }
                }
                catch (Exception ex) // Catch any exceptions that occur during the request
                {
                    // Show a message box with the exception message
                    MessageBox.Show($"Exception occurred: {ex.Message}");
                }
            }
        }
    }
}
