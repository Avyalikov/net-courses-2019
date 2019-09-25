using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stockSimulator.Core.Repositories;
using stockSimulator.Core.Services;
using stockSimulator.WebServ.Repositories;
using System;
using System.IO;

namespace stockSimulator.WebServ
{
    class Startup
    {
        public static IConfigurationRoot configuration;
        public Startup()
        {
            // Create service collection
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
               .AddJsonFile("appsettings.json", false)
               .Build();

            services.AddDbContext<StockSimulatorDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           // services.AddScoped<IClientTableRepository, ClientTableRepository>();
            services.AddSingleton<ClientService>(_ => new ClientService(
                new ClientTableRepository(
                    new StockSimulatorDbContext(
                        new DbContextOptions<StockSimulatorDbContext>()
                        )
                    )
                )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
