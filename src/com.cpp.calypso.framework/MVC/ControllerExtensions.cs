using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace com.cpp.calypso.framework
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="exceptionResult"></param>
        /// <returns></returns>
        public static ActionResult ExceptionResult(this Controller controller, HandleExceptionResult exceptionResult)
        {
            return controller.ExceptionResult(exceptionResult, JsonRequestBehavior.DenyGet);
        }

        public static ActionResult ExceptionResult(this Controller controller, HandleExceptionResult exceptionResult, JsonRequestBehavior behavior)
        {
            //TODO: JSA: colocar en el hander de excepciones
            if (controller.Request.IsAjaxRequest())
            {

                controller.ControllerContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return new JsonResult
                {
                    Data = new { success = false, errors = exceptionResult.Message },
                    ContentType = null, /* contentType */
                    ContentEncoding = null /* contentEncoding */,
                    JsonRequestBehavior = behavior
                };

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, exceptionResult.Message);
            }
        }


        public static ActionResult JsonValidationResult(this Controller controller, ModelStateDictionary modelState)
        {
            return controller.JsonValidationResult(modelState, JsonRequestBehavior.DenyGet);
        }


        public static ActionResult JsonValidationResult(this Controller controller, ModelStateDictionary modelState, JsonRequestBehavior behavior)
        {
            controller.ControllerContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            //TODO: JSA: colocar en el hander de excepciones
            //if (controller.Request.IsAjaxRequest()){

            return new JsonResult
            {
                Data = new { success = false, errors = modelState.ToSerializedDictionary() },
                ContentType = null, /* contentType */
                ContentEncoding = null /* contentEncoding */,
                JsonRequestBehavior = behavior
            };
            //}

        }

        public static ActionResult JsonRedirectResult(this Controller controller, string redirect)
        {
            return controller.JsonRedirectResult(redirect, JsonRequestBehavior.DenyGet);
        }

        public static ActionResult JsonRedirectResult(this Controller controller, string redirect, JsonRequestBehavior behavior)
        {
            //TODO: Direccionamiento AJAX

            controller.ControllerContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Redirect;


            return new JsonResult
            {
                Data = new { success = true, redirect = redirect },
                ContentType = null, /* contentType */
                ContentEncoding = null /* contentEncoding */,
                JsonRequestBehavior = behavior
            };


        }

    }


    public class JsonResponseFactory
    {
        public static object ErrorResponse(string error)
        {
            return new { success = false, errorMessage = error };
        }

        public static object SuccessResponse()
        {
            return new { success = true };
        }

        public static object SuccessResponse(object referenceObject)
        {
            return new { success = true, Object = referenceObject };
        }

    }
}
