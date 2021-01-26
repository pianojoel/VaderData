using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaderData.Models;

namespace VaderData
{
    public class AvgDiffData : WeatherData
    {
        public double IndoorAvgTemp { get; set; }
        public double OutdoorAvgTemp { get; set; }
        public double Difference { get; set; }
    }
}
