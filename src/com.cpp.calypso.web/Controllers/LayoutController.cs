using com.cpp.calypso.framework;
using System;
using System.Web.Mvc;

namespace com.cpp.calypso.web
{
    public class LayoutController : BaseController
    {
        public LayoutController(IHandlerExcepciones manejadorExcepciones) : base(manejadorExcepciones)
        {

        }


        [ChildActionOnly]
        public PartialViewResult Menu(string activeMenu = "")
        {
            throw new NotImplementedException(); 
        }

    }
}