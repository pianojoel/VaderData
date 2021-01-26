using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaderData.Models;

namespace VaderData.Pages
{
    public class FallWinterModel : PageModel
    {
        public List<DisplayData> Data { get; set; }
        public string Message { get; set; }
        public void OnGet()
        {
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
