namespace WebApiTradingServer
{
    using System.Linq;
    using Microsoft.AspNet.OData.Builder;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNet.OData.Formatter;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;
    using Microsoft.OData.Edm;
    using Swashbuckle.AspNetCore.Swagger;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;
    using TradingSoftware.Core.Services;
    using WebApiTradingServer.Repositories;
    using WebApiTradingServer.Services;

    public class Startup
    {
        private readonly IConfiguration Configuration;
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
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
                c.SwaggerDoc(
                    "v1", 
                    new Info {
                        Version = "v1",
                        Title = "WebApiTradingServer",
                        Description = "Simple Web Api for Simple Trading server."
                    });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientManager, ClientManager>();
            services.AddScoped<IBlockOfSharesRepository, BlockOfSharesRepository>();
            services.AddScoped<IBlockOfSharesManager, BlockOfSharesManager>();
            services.AddScoped<IBlockOfSharesRepository, BlockOfSharesRepository>();
            services.AddScoped<IShareManager, ShareManager>();
            services.AddScoped<ISharesRepository, SharesRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped<ILoggerService, LoggerService>();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use((context, next) =>
            {
                context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                return next.Invoke();
            });

            app.UseCors(this.MyAllowSpecificOrigins);

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

        private IEdmModel GetEdmModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            var clients = builder.EntitySet<Client>("ODataClients");
            clients.EntityType.HasKey(entity => entity.ClientID);
            var shares = builder.EntitySet<BlockOfShares>("ODataBlockOfShares");
            shares.EntityType.HasKey(entity => new { entity.ClientID, entity.ShareID });
            var balances = builder.EntitySet<Share>("ODataShare");
            balances.EntityType.HasKey(entity => entity.ShareID);
            var transactions = builder.EntitySet<Transaction>("ODataTransaction");
            transactions.EntityType.HasKey(entity => entity.TransactionID);
            return builder.GetEdmModel();
        }
    }
}
