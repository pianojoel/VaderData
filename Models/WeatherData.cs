using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaderData.Models
{
    public class WeatherData
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Double Temperature { get; set; }
        public Double Humidity { get; set; }
        public string Location { get; set; }
    }
}
