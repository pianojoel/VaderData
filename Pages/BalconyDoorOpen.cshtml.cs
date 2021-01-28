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
                //Get a list of all dates in dataset
                var dates = db.WeatherDataSet
                    .GroupBy(w => w.DateTime.Date)
                    .Where(g => g.Count() > 5)
                    .Select(g => g.Key)
                    .OrderBy(d => d.Date);

                //Speeds up the operation a pinch
                var indoorData = db.WeatherDataSet
                    .Where(w => w.Location == "Inne")
                    .ToList();

                
                foreach (var date in dates)
                {
                    var indoorReading = indoorData
                        .Where(w => w.DateTime.Date == date.Date).OrderBy(t => t.DateTime.TimeOfDay)
                        .ToList();
                    
                    //skip this day if insufficient data
                    if (indoorReading.Count < 6) continue;
                    
                    var start = new DateTime();
                    var end = new DateTime();
                    var totalTime = new TimeSpan();
                    bool isOpen = false;
                    double curAvgTmp;
                    double prvAvgTmp;
                    double thisDiff; 
                    
                    for (int i = 10; i < indoorReading.Count(); i += 1)
                    {
                        //Gets moving average of previous 5 and 10 values
                        curAvgTmp = indoorReading.GetRange(i - 5, 5).Average(r => r.Temperature);
                        prvAvgTmp = indoorReading.GetRange(i - 10, 5).Average(r => r.Temperature);
                       //This is the difference between them
                        thisDiff = curAvgTmp - prvAvgTmp;
                        
                        //Door opens
                        if (!isOpen && Math.Round(thisDiff, 1) <= -0.6) 
                        {
                            start = indoorReading[i].DateTime;
                            isOpen = true;
                        }
                           
                        //Door closes
                        if (isOpen && Math.Round(thisDiff, 1) >= 0.2) 
                        {
                            end = indoorReading[i].DateTime;                            
                            totalTime += end - start;
                            isOpen = false;
                        }


                    }
                   


                    Data.Add(new DisplayData
                    {
                        DateTime = date,
                        TimeDoorOpen = totalTime
                    });
                   
                }

            }
            //List of differences of moving averages
            Data = Data.OrderByDescending(t => t.TimeDoorOpen).ToList();


        }
    }

}
