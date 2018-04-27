using Autofac;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HangFireTest.Startup))]

namespace HangFireTest
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("Connection1");
            GlobalConfiguration.Configuration.UseSqlServerStorage("Connection2");

            var builder = new ContainerBuilder();
            //builder.RegisterInstance(new BasicWriter()).As<IWriter>();

            builder.RegisterType<BasicWriter>().As<IWriter>();

            GlobalConfiguration.Configuration.UseAutofacActivator(builder.Build());

            //var storage = new SqlServerStorage("Connection1");
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions(), storage);

            var storage1 = new SqlServerStorage("Connection1");
            var storage2 = new SqlServerStorage("Connection2");

            app.UseHangfireDashboard("/hangfire1", new DashboardOptions(), storage1);
            app.UseHangfireDashboard("/hangfire2", new DashboardOptions(), storage2);
                        
            //app.UseHangfireDashboard();

            app.UseHangfireServer();
        }
    }
}
