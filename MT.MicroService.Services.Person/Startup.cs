using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MT.MicroService.Core.Repository.Interfaces;
using MT.MicroService.Core.Services;
using MT.MicroService.Core.UnitOfWork;
using MT.MicroService.Data;
using MT.MicroService.Data.Repositories;
using MT.MicroService.Data.UnitOfWork;
using MT.MicroService.Service.Services;
using MT.MicroService.Services.Person.RabbitMQ;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.MicroService.Services.Person
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(sp => new ConnectionFactory() //buradan bir nesne örneði gelecek
            {
                Uri = new Uri(Configuration.GetConnectionString("RabbitMQ"))
            });
            services.AddSingleton<RabbitMQPublisher>();
            services.AddSingleton<RabbitMQClientService>();
            services.AddAutoMapper(typeof(Startup));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>(); //bir request enasýnda birden fazla ihtiyaç olursa ayný nesne örneðini kullanýr.

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IContactInfoService, ContactInfoService>();
            services.AddScoped<IPersonService, PersonService>();

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("NpgConStr"),o=>o.MigrationsAssembly("MT.MicroService.Data")));
            services.AddScoped<DbContext>(provider => provider.GetRequiredService < AppDbContext>());
            
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MT.MicroService.Services.Person", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MT.MicroService.Services.Person v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
