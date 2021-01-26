using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaderData.Models;

namespace VaderData.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<DisplayData> Data { get; set; }
        public List<string> Labels { get; set; }
        public List<double> Temperature { get; set; }
        public List<double> Humidity { get; set; }
        public List<double> Moldrisk { get; set; }


        public void OnGet()
        {
            using (var db = new WdContext())
            {
                Data = db.WeatherDataSet
                        .Where(w => w.Location == "Ute")
                        .GroupBy(d => d.DateTime.Date)
                        .Select(g => new DisplayData
                        {
                            DateTime = g.Key,
                            Temperature = g.Average(g => g.Temperature),
                            Humidity = g.Average(g => g.Humidity)
                        })
                        .OrderBy(w => w.DateTime)
                        .ToList();
            }
            Labels = Data.Select(d => d.DateTime.ToShortDateString()).ToList();
            Temperature = Data.Select(d => d.Temperature).ToList();
            Humidity = Data.Select(d => d.Humidity).ToList();
            Moldrisk = Data.Select(d => Math.Clamp(d.MoldRisk,0,100)).ToList();


        }
    }
}
