namespace WeatherAPI.Constants
{
    public class GeneralConstants
    {
        public const int LocationLimit = 5;
        public const string CityQueryParameter = "city";
        public const string WeatherAPISecretsKey = "WeatherAPISecrets";

        public static Dictionary<string, string> Headers = new Dictionary<string, string>()
                {
                    { "Access-Control-Allow-Origin","*" },
                    { "Access-Control-Allow-Methods","*" },
                    { "Access-Control-Allow-Headers","*" },
                    { "Access-Control-Allow-Credentials","true" }
                };
    }
}