using System;
using System.Configuration;
using Hangfire;
using Hangfire.Mongo;
using Microsoft.Owin;
using Owin;
using HangFirePro;

[assembly: OwinStartup(typeof(Startup))]
namespace HangFirePro
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            var mongoConnectionString = ConfigurationManager.AppSettings["MongoConnection"];

            //DB Connection
            //GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);

            //Mongo Connection
            GlobalConfiguration.Configuration.UseMongoStorage(mongoConnectionString, "Hangfire");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            //For AuthorizationFilter
            //var options = new DashboardOptions();

            //Batchler yalnızca pro versiyonda kullanılabilir.
            //GlobalConfiguration.Configuration.UseBatches();
        }
    }
}