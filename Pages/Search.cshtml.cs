using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using VaderData.Models;

namespace VaderData.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ILogger<SearchModel> _logger;

        public SearchModel(ILogger<SearchModel> logger)
        {
            _logger = logger;
        }

        public List<DisplayData> Data { get; set; }
        [BindProperty]
        public DateTime Date { get; set; }
        [BindProperty]
        public double AvgTmp { get; set; }

        public string Message { get; set; }
        [BindProperty]
        public string Location { get; set; }

        public Dictionary<DateTime, TimeSpan> TimeDoorOpenPerDay { get; set; }
        public void OnGet()
        {
            Data = new List<DisplayData>();
            Date = new DateTime(2016, 5, 31);
           
        }
        public void OnPostSearchDate()
        {
            Data = new List<DisplayData>();
            using (var db = new WdContext())
            {
                if (db.WeatherDataSet.Where(d => d.DateTime.Date == Date.Date && d.Location == Location).Count() > 0)
                {

                    var data = new DisplayData
                    {
                        DateTime = Date,
                        Temperature = db.WeatherDataSet.Where(d => d.DateTime.Date == Date.Date && d.Location == Location).Select(t => t.Temperature).Average(),
                        Humidity = db.WeatherDataSet.Where(d => d.DateTime.Date == Date.Date && d.Location == Location).Select(t => t.Humidity).Average()

                    };
                    Data.Add(data);
                }
                else
                {
                    Message = "Ingen data för " + Date.Date.ToString("d");
                }

               
            }

        }
        public void OnPostFall()
        {

            using (var db = new WdContext())
            {
                var q = db.WeatherDataSet.Where(d => d.Location == "Ute")
                        .GroupBy(d => d.DateTime.Date)
                        .Select(g => new DisplayData
                        {
                            DateTime = g.Key,
                            Temperature = g.Average(g => g.Temperature)
                        })
                        .OrderBy(w => w.DateTime.Date)
                        .ToList();
                for (int i = 0; i < q.Count() - 5; i++)
                {
                    var w = q.GetRange(i, 5);
                    if (w.All(t => t.Temperature < 10))
                    {
                        Data = w.Take(1).ToList();
                        break;
                    }
                }
                if (Data.Count() == 0)
                {
                    Message = "Ingen meteorologisk höst i datan";
                }

            }
        }
        public void OnPostWinter()
        {
            Data = new List<DisplayData>();
            using (var db = new WdContext())
            {
                var q = db.WeatherDataSet.Where(d => d.Location == "Ute")
                        .GroupBy(d => d.DateTime.Date)
                        .Select(g => new DisplayData
                        {
                            DateTime = g.Key,
                            Temperature = g.Average(g => g.Temperature)
                        })
                        .OrderBy(w => w.DateTime.Date)
                        .ToList();
                for (int i = 0; i < q.Count() - 5; i++)
                {
                    var w = q.GetRange(i, 5);
                    if (w.All(t => t.Temperature < 0))
                    {
                        Data = w.Take(1).ToList();
                        break;
                    }
                }

                if (Data.Count() == 0)
                {
                    Message = "Ingen meteorologisk vinter i datan";
                }

            }
        }
       
    }
}
