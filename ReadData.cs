using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VaderData.Models;

namespace VaderData
{
    public static class ReadData
    {
        public static void Read()
        {
            using (var db = new WdContext())
            {
                var data = File.ReadAllText("data.txt").Split('\n');
                int counter = 0;
                foreach (var line in data)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    var items = line.Split(',').Select(i => i.Trim(' ', '\r', '\n')).ToList();

                    var wd = new WeatherData();
                    wd.DateTime = DateTime.Parse(items[0]);
                    wd.Location = items[1];
                    wd.Temperature = double.Parse(items[2], CultureInfo.InvariantCulture);
                    wd.Humidity = double.Parse(items[3]);
                    db.Add(wd);
                    counter++;
                }
                db.SaveChanges();
            }
        }
    }
}
