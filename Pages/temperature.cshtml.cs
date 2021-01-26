using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaderData.Models;

namespace VaderData.Pages
{
    public class temperatureModel : PageModel
    {
        [BindProperty]
        public string Location { get; set; }
        public List<DisplayData> Data { get; set; }
        [BindProperty]
        public bool Desc { get; set; }
        public string Message { get; set; }
        public void OnGet()
        {
            Desc = true;
            Location = "Ute";
            OnPostSortByTemperature();
            //using (var db = new WdContext())
            //{
            //    Data = db.WeatherDataSet
            //        .Where(w => w.Location == "Ute")
            //        .GroupBy(d => d.DateTime.Date)
            //        .Select(g => new OutdoorData
            //        {
            //            DateTime = g.Key,
            //            Temperature = g.Average(g => g.Temperature),
            //            Humidity = g.Average(g => g.Humidity)
            //        })
            //        // MoldRisk = ((Humidity - 78) * (Temperature / 15)) / 0.22
            //        .OrderBy(t => t.Temperature).ToList();
            //}
            //// ((fuktighet - 78) * (temperatur / 15)) / 0,22
        }
        public void OnPost()
        {
            OnPostSortByDate();
        }
        public void OnPostSortByDate()
        {
            Data = GetData();
            if (Desc)
            {
                Data = Data.OrderByDescending(t => t.DateTime).ToList();
                Desc = false;
            }
            else
            {
                Data = Data.OrderBy(t => t.DateTime).ToList();
                Desc = true;
            }

            Message = "Visar data sorterat på datum " + (Desc ? "stigande" : "fallande");       
            
            
        }
        public void OnPostSortByHumidity()
        {
            Data = GetData();
            if (Desc)
            {
                Data = Data.OrderByDescending(t => t.Humidity).ToList();
                Desc = false;
            }
            else
            {
                Data = Data.OrderBy(t => t.Humidity).ToList();
                Desc = true;
            }
            Message = "Visar data sorterat på luftfuktighet " + (Desc ? "stigande" : "fallande");
        }
        public void OnPostSortByMoldRisk()
        {
            Data = GetData();
            if (Desc)
            {
                Data = Data.OrderByDescending(t => t.MoldRisk).ToList();
                Desc = false;
            }
            else
            {
                Data = Data.OrderBy(t => t.MoldRisk).ToList();
                Desc = true;
            }
            Message = "Visar data sorterat på mögelrisk " + (Desc ? "stigande" : "fallande");
        }
        public void OnPostSortByTemperature()
        {
            Data = GetData();
            if (Desc)
            {
                Data = Data.OrderByDescending(t => t.Temperature).ToList();
                Desc = false;
            }
            else
            {
                Data = Data.OrderBy(t => t.Temperature).ToList();
                Desc = true;
            }
            Message = "Visar data sorterat på temperatur " + (Desc ? "stigande" : "fallande");
        }
        public List<DisplayData> GetData()
        {
            using (var db = new WdContext())
            {


                return db.WeatherDataSet
                        .Where(w => w.Location == Location)
                        .GroupBy(d => d.DateTime.Date)
                        .Select(g => new DisplayData
                        {
                            DateTime = g.Key,
                            Temperature = g.Average(g => g.Temperature),
                            Humidity = g.Average(g => g.Humidity)
                        }).ToList();
            }
        }
        public void OnPostFall()
        {

            using (var db = new WdContext())
            {
                //var q = db.WeatherDataSet.Where(d => d.Location == "Ute")
                //        .GroupBy(d => d.DateTime.Date)
                //        .Select(g => new DisplayData
                //        {
                //            DateTime = g.Key,
                //            Temperature = g.Average(g => g.Temperature)
                //        })
                //        .OrderBy(w => w.DateTime.Date)
                //        .ToList();
                Location = "Ute";
                var q = GetData().OrderBy(w => w.DateTime.Date).ToList();
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
                else
                {
                    Message = "Visar datum för meteorologisk höst, den första av fem åtföljande dagar med medeltemperatur under 10 grader C";
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
                else
                {
                    Message = "Visar datum för meteorologisk vinter, den första av fem åtföljande dagar med medeltemperatur under 0 grader C";
                }

            }
        }
    }
   
}
