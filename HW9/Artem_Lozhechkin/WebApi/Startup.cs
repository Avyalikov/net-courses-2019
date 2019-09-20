namespace TradingApp.WebApi
{
    using Microsoft.AspNet.OData.Builder;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNet.OData.Formatter;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;
    using Microsoft.OData.Edm;
    using StructureMap;
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using System.Linq;
    using TradingApp.Core.Models;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOData();
            services.AddMvc();
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Trading Web API",
                    Description = "Web API for trading."
                });
            });
            var container = new Container();

            container.Configure(config =>
            {
                config.AddRegistry(new WebApiRegistry());
                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc(options =>
            {
                options.EnableDependencyInjection();
                options.Select().Expand().Filter().OrderBy().MaxTop(100);
                options.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trading Web API v1");
            });
        }
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<TraderEntity>("Traders");
            builder.EntitySet<CompanyEntity>("Companies");
            builder.EntitySet<ShareEntity>("Shares");
            builder.EntitySet<TransactionEntity>("Transactions");

            return builder.GetEdmModel();
        }
    }
}
