using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class Secrets
    {
        public string WeatherApiAppId { get; set; }
        public string RedisPrimaryEndPointUrl { get; set; }
        public string RedisReaderEndPointUrl { get; set; }
        public bool IsCacheEnabled { get; set; }
        public int CacheTTLSecond { get; set; }
    }
}
