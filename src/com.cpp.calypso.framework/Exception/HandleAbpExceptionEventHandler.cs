using Abp.Dependency;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Inteceptar excepciones de Abp, por medio de event 
    /// </summary>
    public class HandleAbpExceptionEventHandler : IEventHandler<AbpHandledExceptionData>, ITransientDependency
    {
        private readonly IHandlerExcepciones handlerExcepciones;

        public HandleAbpExceptionEventHandler(IHandlerExcepciones handlerExcepciones)
        {
            this.handlerExcepciones = handlerExcepciones;
        }

        public void HandleEvent(AbpHandledExceptionData eventData)
        {
            handlerExcepciones.HandleException(eventData.Exception);
            ////wrapper Elmah
            //ElmahExtension.LogToElmah(eventData.Exception);
 
        }
    }
}
