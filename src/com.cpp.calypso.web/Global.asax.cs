using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;
using com.cpp.calypso.web.App_Start;
using com.cpp.calypso.web.Util;
using System;

namespace com.cpp.calypso.web
{
    public class MvcApplication : AbpWebApplication<WebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            //Configurar Logs nlog
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseCustomNLog()
            );

            //Logs with log4net. is not Unified internal logs
            //AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
            //    f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
            //);

            base.Application_Start(sender, e);
        }

      
    }
}
