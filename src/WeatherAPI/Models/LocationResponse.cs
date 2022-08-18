using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class LocationResponse
    {
        public string Name { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }
}
