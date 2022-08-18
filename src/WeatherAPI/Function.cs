using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.SecretsManager.Extensions.Caching;
using Newtonsoft.Json;
using Refit;
using StackExchange.Redis;
using System.Net;
using WeatherAPI.Clients;
using WeatherAPI.Constants;
using WeatherAPI.Mappers;
using WeatherAPI.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WeatherAPI;

public class Functions
{
    public IOpenWeatherAPI _openWeatherAPIClient;

    private SecretsManagerCache _secretsManagerCache = new SecretsManagerCache(new SecretCacheConfiguration
    {
        CacheItemTTL = 300000 //5 mininutes
    });

    private const string WeatherAPISecrets = "WeatherAPISecrets";

    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions()
    {
        _openWeatherAPIClient = RestService.For<IOpenWeatherAPI>(UrlConstants.BaseUrl);
    }

    /// <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The API Gateway response.</returns>
    public async Task<APIGatewayProxyResponse> GetCurrentWeather(APIGatewayProxyRequest request, ILambdaContext context)
    {
        if (request.QueryStringParameters == null || !request.QueryStringParameters.TryGetValue(GeneralConstants.CityQueryParameter, out var cityName))
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = ErrorMessages.CityNameCannotBeNull,
                Headers = GeneralConstants.Headers
            };
        }

        var secretsResponse = await _secretsManagerCache.GetSecretString(WeatherAPISecrets);
        var secrets = JsonConvert.DeserializeObject<Secrets>(secretsResponse);

        var IsCacheEnabled = secrets.IsCacheEnabled;
        ConnectionMultiplexer redis;
        IDatabase? redisDb = null;
        var weatherCacheKey = $"Weather:{cityName}";
        string weatherFromCache = string.Empty;

        if (IsCacheEnabled)
        {
            redis = ConnectionMultiplexer.Connect($"{secrets.RedisPrimaryEndPointUrl},{secrets.RedisReaderEndPointUrl}");
            redisDb = redis.GetDatabase();
            weatherFromCache = await redisDb.StringGetAsync(weatherCacheKey);
        }

        if (!string.IsNullOrEmpty(weatherFromCache))
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(weatherFromCache),
                Headers = GeneralConstants.Headers
            };
        }
        else
        {
            var locations = await _openWeatherAPIClient.GetLocations(cityName, GeneralConstants.LocationLimit, secrets.WeatherApiAppId);
            var popularLocation = locations.FirstOrDefault();
            if (popularLocation == null)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = ErrorMessages.CityNameDidntMatch,
                    Headers = GeneralConstants.Headers
                };
            }

            var weatherResponse = await _openWeatherAPIClient.GetWeather(popularLocation.Lat, popularLocation.Lon, secrets.WeatherApiAppId);
            var weather = weatherResponse.ToViewModel();

            if (IsCacheEnabled)
            {
                await redisDb.StringSetAsync(weatherCacheKey, JsonConvert.SerializeObject(weather), TimeSpan.FromSeconds(secrets.CacheTTLSecond));
            }

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(weather),
                Headers = GeneralConstants.Headers
            };
        }
    }
}