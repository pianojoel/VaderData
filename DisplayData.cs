using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaderData.Models;

namespace VaderData
{
    public class DisplayData : WeatherData
    {
        public double MoldRisk => Math.Clamp(((Humidity - 78) * (Math.Abs(Temperature) / 15)) / 0.22, 0, 100);
        public TimeSpan TimeDoorOpen { get; set; }
    }
}
