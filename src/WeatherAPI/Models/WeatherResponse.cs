using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class WeatherResponse
    {
        public string Name { get; set; }
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Wind Wind { get; set; }
    }

    public class Main
    {
        public decimal Temp { get; set; }
        public decimal Pressure { get; set; }
        public decimal Humidity { get; set; }
    }

    public class Weather
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class Wind
    {
        public decimal Speed { get; set; }
        public int Deg { get; set; }
    }
}
