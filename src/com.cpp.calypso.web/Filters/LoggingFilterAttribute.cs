using com.cpp.calypso.framework;
using CommonServiceLocator;
using System.Linq;
using System.Web.Mvc;


namespace  com.cpp.calypso.web
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        static readonly ILogger log =
        ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(LoggingFilterAttribute));

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
           log.DebugFormat("Controller {0}, Action {1}, Arguments {2}",
           filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName,
           filterContext.ActionDescriptor.ActionName,
           string.Join(", ", filterContext.ActionParameters.Select(e => string.Format("Param [{0}] = Value [{0}]", e.Key, e.Value))));

        }

 
    }
}
