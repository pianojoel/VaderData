using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaderData.Models
{
    public class WdContext : DbContext
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;database=DbWebTest;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<WeatherData> WeatherDataSet { get; set; }
    }
}
