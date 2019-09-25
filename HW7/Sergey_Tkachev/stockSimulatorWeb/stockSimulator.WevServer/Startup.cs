using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using stockSimulator.WevServer.Dependecies;
using StructureMap;
using System;
using System.Web.Http;

namespace stockSimulator.WevServer
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var container = new Container();

            container.Configure(config =>
            {
                config.AddRegistry(new StockSimulatorRegistry());
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
           
        }
    }
}
