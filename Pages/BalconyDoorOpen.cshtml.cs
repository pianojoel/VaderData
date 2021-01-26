using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaderData.Models;

namespace VaderData.Pages
{
    public class BalconyDoorOpenModel : PageModel
    {
        public List<DisplayData> Data { get; set; }
        public void OnGet()
        {
            Data = new List<DisplayData>();
            using (var db = new WdContext())
            {
                var dates = db.WeatherDataSet
                    .GroupBy(w => w.DateTime.Date)
                    .Where(g => g.Count() > 5)
                    .Select(g => g.Key)
                    .OrderBy(d => d.Date);


                var indoorData = db.WeatherDataSet
                    .Where(w => w.Location == "Inne")
                    .ToList();

                foreach (var date in dates)
                {
                    var indoorReading = indoorData.Where(w => w.DateTime.Date == date.Date).OrderBy(t => t.DateTime.TimeOfDay).ToList();
                    if (indoorReading.Count < 6) continue;
                    var start = new DateTime();
                    var end = new DateTime();
                    var totalTime = new TimeSpan();
                    bool isOpen = false;
                   
                    double thisDiff = indoorReading[5].Temperature - indoorReading[0].Temperature;
                    
                    for (int i = 5; i < indoorReading.Count(); i += 5)
                    {
                        thisDiff = indoorReading[i].Temperature - indoorReading[i - 5].Temperature;
                        if (!isOpen && Math.Round(thisDiff, 1) <= -0.2) //Door opens
                        {
                            start = indoorReading[i].DateTime;
                            isOpen = true;
                           
                        }
                        if (isOpen && Math.Round(thisDiff, 1) >= 0.2) //Door closes
                        {
                            end = indoorReading[i].DateTime;
                            isOpen = false;
                            totalTime += end - start;
                           
                        }


                    }
                   


                    Data.Add(new DisplayData
                    {
                        DateTime = date,
                        TimeDoorOpen = totalTime
                    });
                   
                }

            }

            Data = Data.OrderByDescending(t => t.TimeDoorOpen).ToList();


        }
    }

}
