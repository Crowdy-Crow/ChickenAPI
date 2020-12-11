using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ChickensAPI.Entities;

namespace ChickensAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet <User> Users { get; set; }
        public DbSet<Chicken> Chickens { get; set; }
        public DbSet<Egg> Eggs { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { Database.EnsureCreated(); }
        
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
