using Refit;
using WeatherAPI.Constants;
using WeatherAPI.Models;

namespace WeatherAPI.Clients
{
    public interface IOpenWeatherAPI
    {
        /// <summary>
        /// Get geographical coordinates (lat, lon) by using name of the location(city name or area name).
        /// </summary>
        /// <param name="q">City bame</param>
        /// <param name="limit">Number of the locations in the API response</param>
        /// <param name="appid">Unique API key</param>
        /// <returns></returns>
        [Get(UrlConstants.GetLocations)]
        Task<List<LocationResponse>> GetLocations([Query] string q, [Query] int limit, [Query] string appid);

        /// <summary>
        /// Get current weather data for any location on Earth. 
        /// </summary>
        /// <param name="lat">Geographical coordinates (latitude)</param>
        /// <param name="lon">Geographical coordinates (longitude)</param>
        /// <param name="appid">Unique API key</param>
        /// <returns></returns>
        [Get(UrlConstants.GetWeather)]
        Task<WeatherResponse> GetWeather([Query] decimal lat, [Query] decimal lon, [Query] string appid);
    }
}