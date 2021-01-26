using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaderData.Models;

namespace VaderData.Pages
{
    public class DifferencesModel : PageModel
    {
        public List<AvgDiffData> Data { get; set; }
        [BindProperty]
        public bool Desc { get; set; }
        public void OnGet()
        {
            Desc = true;
            OnPostAvgDiff();
        }
        public void OnPostAvgDiff()
        {
            using (var db = new WdContext())
            {
                var IndoorAvg = db.WeatherDataSet
                    .Where(w => w.Location == "Inne")
                    .GroupBy(d => d.DateTime.Date)
                    .Select(g => new DisplayData
                    {
                        DateTime = g.Key,
                        Temperature = g.Average(g => g.Temperature)
                    })
                    .OrderBy(t => t.DateTime).ToList();

                var OutDoorAvg = db.WeatherDataSet
                    .Where(w => w.Location == "Ute")
                    .GroupBy(d => d.DateTime.Date)
                    .Select(g => new DisplayData

                    {
                        DateTime = g.Key,
                        Temperature = g.Average(g => g.Temperature)
                    })
                    .OrderBy(t => t.DateTime).ToList();

                Data = IndoorAvg.Select(i => new AvgDiffData
                {
                    DateTime = i.DateTime,
                    IndoorAvgTemp = i.Temperature,
                    OutdoorAvgTemp = OutDoorAvg.Where(o => o.DateTime.Date == i.DateTime.Date).First().Temperature,
                    Difference = OutDoorAvg.Where(o => o.DateTime.Date == i.DateTime.Date).First().Temperature - i.Temperature
                })
                    .ToList();
                if (Desc)
                {
                    Data = Data.OrderByDescending(d => d.Difference).ToList();
                    Desc = false;
                }
                else
                {
                    Data = Data.OrderBy(d => d.Difference).ToList();
                    Desc = true;
                }
                    
            }
        }
    }
   
}
