using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftFactoryBusinessLogic;
using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryDatabaseImplement.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AircraftFactoryRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientLogic, ClientLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IAircraftLogic, AircraftLogic>();
            services.AddTransient<IMessageInfoLogic, MessageInfoLogic>();
            services.AddTransient<IStockLogic, StockLogic>();
            services.AddTransient<IPartLogic, PartLogic>();
            services.AddTransient<MainLogic>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
