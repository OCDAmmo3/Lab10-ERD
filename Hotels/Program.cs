using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hotels.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotels
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            UpdateDatabase(host.Services);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void UpdateDatabase(IServiceProvider services)
        {
            using (var serviceScope = services.CreateScope())
            {
                using (var db = serviceScope.ServiceProvider.GetService<HotelDbContext>())
                {
                    db.Database.Migrate();
                }
            }
        }
    }
}
