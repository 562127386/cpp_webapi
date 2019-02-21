using Abp.Web;
using Castle.Facilities.Logging;
using com.cpp.calypso.webapi.App_Start;
using System;

namespace com.cpp.calypso.webapi
{
    public class MvcApplication : AbpWebApplication<WebApiModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            //TODO: Pendiente...
            //Configurar Logs nlog
            //AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
            //    f => f.UseCustomNLog()
            //);

            //Logs with log4net. is not Unified internal logs
            //AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
            //    f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
            //);

            base.Application_Start(sender, e);
        }

      
    }
}
