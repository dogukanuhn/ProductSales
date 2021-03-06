using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Filters;

namespace ProductSales.API
{
#pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                     .Enrich.FromLogContext()
                     .MinimumLevel.Information()
                     .Filter.ByExcluding(Matching.FromSource("Serilog"))
                     .Filter.ByExcluding(Matching.FromSource("Hangfire"))
                     .WriteTo.Console()
                     .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
#pragma warning restore CS1591
}
