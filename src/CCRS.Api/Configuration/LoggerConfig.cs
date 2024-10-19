using CCRS.Api.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CCRS.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddElmahIo(o =>
            //{
            //    o.ApiKey = "83a663d3dae74352b6e2ca6509650314";
            //    o.LogId = new Guid("75924990-8e0e-45f2-b751-55803a66f5e5");
            //});

            //services.AddHealthChecks()
            //    .AddElmahIoPublisher(options =>
            //    {
            //        options.ApiKey = "83a663d3dae74352b6e2ca6509650314";
            //        options.LogId = new Guid("75924990-8e0e-45f2-b751-55803a66f5e5");
            //        options.HeartbeatId = "API Patients";

            //    })
            //    .AddCheck("Patients", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
            //    .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            //services.AddHealthChecksUI()
            //    .AddSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            //app.UseElmahIo();

            return app;
        }
    }

}
