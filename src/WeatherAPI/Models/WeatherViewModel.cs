using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class WeatherViewModel
    {
        public string City { get; set; }
        public string Temperature { get; set; }
        public WeatherCondition WeatherCondition  { get; set; }
        public WindView Wind { get; set; }
    }

    public class WeatherCondition
    {
        public string Type { get; set; }
        public decimal Pressure { get; set; }
        public decimal Humidity { get; set; }
    }

    public class WindView
    {
        public string Speed { get; set; }
        public string Direction { get; set; }
    }

}
