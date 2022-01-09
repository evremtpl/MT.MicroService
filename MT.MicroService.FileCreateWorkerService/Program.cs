using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MT.MicroService.FileCreateWorkerService.Models;
using MT.MicroService.FileCreateWorkerService.Service;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.MicroService.FileCreateWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration Configuration = hostContext.Configuration;
                    services.AddDbContext<PhoneBookReportContext>(opt =>
                    {
                        opt.UseNpgsql(Configuration.GetConnectionString("WorkerCnnStr"));
                    });
                    services.AddSingleton<RabbitMQClientService>();
                    services.AddSingleton(sp => new ConnectionFactory() //buradan bir nesne örneði gelecek
                    {
                        Uri = new Uri(Configuration.GetConnectionString("RabbitMQ"))
                    });
                    services.AddHostedService<Worker>();
                });
    }
}
