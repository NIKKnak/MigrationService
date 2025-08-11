using Updater.Data;
using Updater.Services;
using Microsoft.EntityFrameworkCore;

namespace Updater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddHostedService<CurrencyUpdateService>();

            var host = builder.Build();
            host.Run();
        }
    }
}