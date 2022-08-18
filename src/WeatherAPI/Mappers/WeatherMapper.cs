using WeatherAPI.Models;

namespace WeatherAPI.Mappers
{
    public static class WeatherMapper
    {
        public static WeatherViewModel ToViewModel(this WeatherResponse response)
        {
            return new WeatherViewModel
            {
                City = response.Name,
                Temperature = $"{response.Main.Temp} °C",
                WeatherCondition = new WeatherCondition
                {
                    Type = response.Weather?.FirstOrDefault()?.Description,
                    Pressure = response.Main.Pressure,
                    Humidity = response.Main.Humidity,
                },
                Wind = new WindView
                {
                    Speed = $"{response.Wind.Speed} km/h",
                    Direction = GetDirectionByDegree(response.Wind.Deg)
                }
            };
        }

        private static string GetDirectionByDegree(int degree)
        {
            string[] caridnals = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW", "N" };
            var test = caridnals[(int)Math.Round(((double)degree * 10 % 3600) / 225)];
            return test.ToString();
        }
    }
}