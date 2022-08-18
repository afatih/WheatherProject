namespace WeatherAPI.Constants
{
    public class GeneralConstants
    {
        public const int LocationLimit = 5;
        public const string CityQueryParameter = "city";

        public static Dictionary<string, string> Headers = new Dictionary<string, string>()
                {
                    { "Access-Control-Allow-Origin","*" },
                    { "Access-Control-Allow-Methods","GET, PUT, PATCH, POST, DELETE, OPTIONS" },
                    { "Access-Control-Allow-Headers","Authorization, Content-Type" },
                    { "Access-Control-Allow-Credentials","true" }
                };
    }
}