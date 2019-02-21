using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Linq;
using CommonServiceLocator;
using com.cpp.calypso.framework;

namespace  com.cpp.calypso.web
{
    public class LoggingFilterApiAttribute : ActionFilterAttribute
    {
        static readonly ILogger log =
        ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(LoggingFilterAttribute));

        

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            log.DebugFormat("Controller {0}, Action {1}, Arguments {2}",
         filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName,
         filterContext.ActionDescriptor.ActionName,
         string.Join(", ", filterContext.ActionArguments.Select(e => string.Format("Param [{0}] = Value [{0}]", e.Key, e.Value))));

        }

         

 
    }
}
